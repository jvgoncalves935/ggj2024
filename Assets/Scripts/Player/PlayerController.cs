using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float horizontalMove = 0f;
    [SerializeField] private float verticalMove = 0f;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private float runMultipier = 1.6f;
    [SerializeField] private float attackSpeedMultiplier = 0.3f;

    [Header("Gravity Settings")]
    [SerializeField] private float raycastGroundDistance = 1.0f;
    [SerializeField] private Vector2 groundCheckBoxSize;
    [SerializeField] private float groundCheckCastDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] private PhysicsMaterial2D fullFrictionMaterial;
    [SerializeField] private bool isGrounded = false;

    [Header("Health Settings")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private bool gamePaused;
    [SerializeField] private Checkpoint checkPoint;
    [SerializeField] private int coins = 0;
    [SerializeField] private int playerCurrentHealth = 5;
    [SerializeField] private float invencibilityTime = 1.0f;



    
    private Rigidbody2D rb;
    private Animator animator;
    private bool isPlayerHit = false;
    private SpriteRenderer spriteRenderer;
    private bool playerKilled = false;
    private int playerFullHealth;
    private GameObject swordCollider;
    private bool isPlayerAttacking = false;


    [SerializeField] public static GameObject instanciaPlayerController;
    private static PlayerController _instanciaPlayerController;
    public static PlayerController InstanciaPlayerController {
        get {
            if(_instanciaPlayerController == null) {
                _instanciaPlayerController = instanciaPlayerController.GetComponent<PlayerController>();
            }
            return _instanciaPlayerController;
        }
    }

    private void Awake() {
        instanciaPlayerController = FindObjectOfType<PlayerController>().gameObject;
        playerFullHealth = playerCurrentHealth;
    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gamePaused = false;
        swordCollider = transform.Find("SwordCollider").gameObject;
        swordCollider.SetActive(false);
    }   

    // Update is called once per frame
    void Update() {
        GetGamePaused();
        
        if (!gamePaused)
        {
            IsGroundedCheck();
            
            GetHorizontalVerticalMove();
            //IsSlopeCheck();

            CharacterMoveHorizontal();
            CharacterMoveVertical();
            GetSprint();
            FlipSprite();
            PlayerAttack();
            GetAnimations();
            PlayerKilledState();
        }
    }

    void GetHorizontalVerticalMove() {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    void FlipSprite() {
        if(!isPlayerAttacking) {
            if((isFacingRight && horizontalMove < 0.0f) || (!isFacingRight && horizontalMove > 0.0f)) {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }
    }

    void CharacterMoveHorizontal() {
        rb.velocity = new Vector2(horizontalMove * walkSpeed, rb.velocity.y);
        if (isRunning)
        {
            rb.velocity = new Vector2(rb.velocity.x * runMultipier, rb.velocity.y);
        }
    }

    void CharacterMoveVertical() {
        //Pulo segurando o botão
        if(Input.GetButtonDown("Jump") && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //O player já está pulando mas soltou o botão
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0.0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    private void IsGroundedCheck() {
        if(Physics2D.BoxCast(transform.position, groundCheckBoxSize, 0, -transform.up, groundCheckCastDistance, groundLayer)) {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckCastDistance, groundCheckBoxSize);
    }

    private void GetSprint() {
        if(Input.GetButton("Run")) {
            isRunning = true;
        } else {
            isRunning = false;
        }
    }

    public void AddCoin() {
        coins++;
    }

    public void RestartCheckpoint() {
        transform.position = checkPoint.Position();

        //Resetar a gravidade do player.
        rb.velocity = new Vector2(0, 0);

        PlayerAddHealth(playerFullHealth);
    }

    public void RestartCoins() {
        coins = 0;
    }

    public void SetCheckpoint(Checkpoint newCheckpoint) {
        checkPoint = newCheckpoint;
    }

    private void GetAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("OnGround", isGrounded);
        animator.SetBool("Running", isRunning);
        animator.SetBool("Paused", gamePaused);
        animator.SetBool("Killed", playerKilled);
        animator.SetBool("AttackSwordGround", isPlayerAttacking);
    }

    private void GetGamePaused()
    {
        if (Time.timeScale > 0)
        {
            gamePaused = false;
        }
        else
        {
            gamePaused = true;
        }
    }

    public void PlayerDamage(int damage)
    {
        StartCoroutine(PlayerDamageCoroutine(damage));
    }

    public bool IsPlayerHit()
    {
        return isPlayerHit;
    }

    private void NormalizePlayerDamage()
    {
        if (playerCurrentHealth <= 0)
        {
            playerCurrentHealth = 0;
        }
    }

    private IEnumerator PlayerDamageCoroutine(int damage)
    {
        playerCurrentHealth -= damage;
        NormalizePlayerDamage();

        healthBar.SetHealth(playerCurrentHealth);

        if (!IsPlayerKilled())
        {
            isPlayerHit = true;
            PlayerDamagedAnimation();
            yield return new WaitForSeconds(invencibilityTime);
            isPlayerHit = false;
        }else{
            KillPlayer();
            yield return new WaitForSeconds(3.0f);
            ResetScene.InstanciaResetScene.Reset();
        }
        
    }

    private void PlayerDamagedAnimation()
    {
        StartCoroutine(PlayerDamagedCoroutine());
    }

    private IEnumerator PlayerDamagedCoroutine()
    {
        bool isEnabled = true;
        while (isPlayerHit && !playerKilled)
        {
            isEnabled = !isEnabled;
            spriteRenderer.enabled = isEnabled;
            yield return new WaitForSeconds(0.02f);
        }
        spriteRenderer.enabled = true;
        
    }

    private bool IsPlayerKilled()
    {
        return playerCurrentHealth <= 0;
    }

    private void KillPlayer()
    {
        playerKilled = true;
        isPlayerHit = true;
        playerCurrentHealth = 0;
    }

    private void PlayerKilledState()
    {
        if (playerKilled)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void RestartPlayer()
    {
        playerKilled = false;
        playerCurrentHealth = playerFullHealth;
        isPlayerHit = false;
        healthBar.SetHealth(playerCurrentHealth);
    }

    public void PlayerAddHealth(int amount)
    {
        if(playerCurrentHealth + amount > playerFullHealth)
        {
            playerCurrentHealth = playerFullHealth;
        }
        else
        {
            playerCurrentHealth += amount;
        }

        healthBar.SetHealth(playerCurrentHealth);
    }

    public int GetPlayerMaxHealth()
    {
        return playerFullHealth;
    }

    private void PlayerAttack()
    {
        if (!isPlayerAttacking)
        {
            if (Input.GetMouseButtonDown(0) && isGrounded)
            {
                InitPlayerAttack();
            }
        }
        else
        {
            //rb.velocity = new Vector2(horizontalMove * walkSpeed * attackSpeedMultiplier, rb.velocity.y);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void InitPlayerAttack()
    {
        StartCoroutine(InitPlayerAttackCoroutine());
    }

    private IEnumerator InitPlayerAttackCoroutine()
    {
        rb.velocity = new Vector2(0, 0);
        isPlayerAttacking = true;
        //Sacando a espada
        yield return new WaitForSeconds(0.1f);

        //Aplicando dano
        swordCollider.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        //Deadframes espada e fim animação
        swordCollider.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        //yield return new WaitForSeconds(1.0f);
        isPlayerAttacking = false;

    }
}

