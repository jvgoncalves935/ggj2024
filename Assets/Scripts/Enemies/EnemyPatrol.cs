using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy
{
    [SerializeField] private bool isFacingRight;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pauseTime;
    [SerializeField] private bool stopped = false;
    [SerializeField] private float[] pointPositions;
    [SerializeField] private int enemyDamage = 1;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int currentPointIndex = 0;
    private float currentMoveSpeed;
    private Vector2 startingPosition;
    private bool isFacingRightStart;
    private BoxCollider2D boxCollider;
    
    private Animator animator;

    private void Awake()
    {
        SetBaseDamage(enemyDamage);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        startingPosition = new Vector2(transform.position.x,transform.position.y);
        isFacingRightStart = isFacingRight;

        InitEdgePositions();

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!killed) {
            EnemyMovement();
            CheckEdgePatrol();
        }
        GetAnimations();
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
        spriteRenderer.flipX = !spriteRenderer.flipX;
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
        Transform points = transform.Find("Points");

        pointPositions[0] = points.GetChild(0).position.x;
        pointPositions[1] = points.GetChild(1).position.x;

        points.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        points.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Player" && !PlayerController.InstanciaPlayerController.IsPlayerHit()) {
            DamagePlayer(enemyDamage);
        }
    }

    public override void Restart() {
        transform.position = startingPosition;
        isFacingRight = isFacingRightStart;
        stopped = false;
        killed = false;
        boxCollider.enabled = true;
    }

    private void GetAnimations()
    {
        animator.SetFloat("Speed", currentMoveSpeed);
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
    }

    public override void SetBaseDamage(int baseDamage) {
        damage = baseDamage;
    }

}
