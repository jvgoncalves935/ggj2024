using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyPatrolSword: Enemy
{
    [SerializeField] private bool isFacingRight;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pauseTime;
    [SerializeField] private bool stopped = false;
    [SerializeField] private float[] pointPositions;
    [SerializeField] private int enemyBodyDamage = 1;
    [SerializeField] private int enemySwordDamage = 2;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int currentPointIndex = 0;
    private float currentMoveSpeed;
    private Vector2 startingPosition;
    private bool isFacingRightStart;
    private BoxCollider2D boxCollider;
    private bool isAttacking = false;
    private float attackRange = 4.0f;
    private float playerDistance;
    private Vector2 playerPosition;
    private bool isCooldownAttack = false;

    private PlayerController player;
    private GameObject swordCollider;
    private float enemyFlippedPrevious;
    private float previousMoveSpeed;
    private Vector3 initialScale;
    private float initialVelocityX;

    private Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        SetBaseDamage(enemySwordDamage);
    }
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        startingPosition = new Vector2(transform.position.x, transform.position.y);
        isFacingRightStart = isFacingRight;

        player = PlayerController.InstanciaPlayerController;
        swordCollider = transform.Find("SwordCollider").gameObject;
        swordCollider.SetActive(false);

        
        InitEdgePositions();
        currentMoveSpeed = moveSpeed;
        initialScale = transform.localScale;

        initialVelocityX = GetInitialVelocity();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(!killed) {
            EnemyMovement();
            CheckEdgePatrol();
            CheckAttack();
        }
        GetAnimations();
    }

    private float GetInitialVelocity()
    {
        if (isFacingRight){
            return moveSpeed;
        }else{
            return -moveSpeed;
        }
    }

    private void EnemyMovement() {
        if(isFacingRight) {
            rb.velocity = new Vector2(currentMoveSpeed, 0);
        } else {
            rb.velocity = new Vector2(-currentMoveSpeed, 0);
        }
    }

    private void CheckEdgePatrol() {
        if(!stopped) {
            if((!isFacingRight && transform.position.x <= pointPositions[currentPointIndex]) ||
                (isFacingRight && transform.position.x >= pointPositions[currentPointIndex])) {
                StopMovement();
            }
        }
    }
    private void FlipSprite() {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * -1,scale.y,scale.z);
    }

    private void FlipSprite(float x)
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(x, scale.y,scale.z);
    }

    private int GetFlipPointDirection()
    {
        if (isFacingRight)
        {
            return 1;
        }
        return 0;
    }

    private void FlipPointDirection() {
        if(currentPointIndex == 0) {
            currentPointIndex = 1;
            isFacingRight = true;
        } else {
            currentPointIndex = 0;
            isFacingRight = false;
        }
    }

    private IEnumerator StopMovementCoroutine() {
        stopped = true;

        currentMoveSpeed = 0;
        yield return new WaitForSeconds(pauseTime);
        FlipSprite();
        FlipPointDirection();

        currentMoveSpeed = moveSpeed;

        stopped = false;

    }

    private void StopMovement() {
        StartCoroutine(StopMovementCoroutine());
    }

    private void InitEdgePositions() {
        pointPositions = new float[2];
        UnityEngine.Transform points = transform.Find("Points");

        pointPositions[0] = points.GetChild(0).position.x;
        pointPositions[1] = points.GetChild(1).position.x;

        points.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        points.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Player" && !PlayerController.InstanciaPlayerController.IsPlayerHit()) {
            DamagePlayer(enemyBodyDamage);
        }
    }

    public override void Restart() {
        transform.position = startingPosition;
        isFacingRight = isFacingRightStart;
        currentPointIndex = GetFlipPointDirection();
        stopped = false;
        killed = false;
        boxCollider.enabled = true;
        isCooldownAttack = false;
        isAttacking = false;
        transform.localScale = initialScale;
        currentMoveSpeed = moveSpeed;
        rb.velocity = new Vector2(initialVelocityX, 0);

    }

    private void GetAnimations() {
        animator.SetFloat("Speed", currentMoveSpeed);
        animator.SetBool("Attack", isAttacking);
        animator.SetBool("Killed", killed);
    }

    public override void Kill() {
        StartCoroutine(KillCoroutine());
    }

    private IEnumerator KillCoroutine() {

        DisableEnemy();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void DisableEnemy() {
        killed = true;
        rb.velocity = new Vector2(0, 0);
        boxCollider.enabled = false;
        isCooldownAttack = false;
        isAttacking = false;
    }

    private Vector2 GetPlayerPosition() {
        return new Vector2(player.transform.position.x,player.transform.position.y);
    }

    private void CheckAttack() {
        playerPosition = GetPlayerPosition();
        playerDistance = Vector2.Distance(transform.position,playerPosition);
        
        //O ataque será realizado com o jogador perto do inimigo e fora do cooldown.
        if(playerDistance < attackRange && !isCooldownAttack) {
            
            //O ataque será feito apenas uma vez.
            if (!isAttacking)
            {
                GetPreAttackStats();
                FlipTowardsPlayer(playerPosition.x, transform.position.x);
                Attack();
            }
        }

        if (isAttacking){
            rb.velocity = new Vector2(0, 0);
        }

    }

    private void FlipTowardsPlayer(float playerPosition, float enemyPosition)
    {
        if(playerPosition < enemyPosition)
        {
            FlipSprite(initialScale.x);
        }
        else
        {
            FlipSprite(-initialScale.x);
        }
        
    }

    private void Attack() {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine() {
        isAttacking = true;
        //Pausa para atacar
        yield return new WaitForSeconds(0.5f);

        //Ataque com espada
        swordCollider.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        //Deadframes ataque
        swordCollider.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        //Fim ataque (com cooldown)
        SetPreAttackStats();
        isAttacking = false;
        CooldownAttack(1.0f);
    }

    private void CooldownAttack(float time)
    {
        StartCoroutine(CooldownAttackCoroutine(time));
    }

    private IEnumerator CooldownAttackCoroutine(float time)
    {
        isCooldownAttack = true;
        yield return new WaitForSeconds(time);
        isCooldownAttack = false;
    }

    public override void SetBaseDamage(int baseDamage) {
        damage = baseDamage;
    }

    private void GetPreAttackStats()
    {
        previousMoveSpeed = currentMoveSpeed;
        enemyFlippedPrevious = transform.localScale.x;
    }

    private void SetPreAttackStats()
    {
        currentMoveSpeed = previousMoveSpeed;
        FlipSprite(enemyFlippedPrevious);
    }

}
