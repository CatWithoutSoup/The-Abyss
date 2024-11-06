using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

//[RequireComponent(typeof(SpriteRenderer),typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private UnityEvent OnGroundTouch;
    [SerializeField] public UnityEvent OnDash;
    [SerializeField] private UnityEvent OnWallGrabStart;
    [SerializeField] private UnityEvent OnWallGrabEnd;

    public Rigidbody2D rb;
    private SpriteRenderer sr;
    public Vector2 moveVector;
    private Animator anim;

    [SerializeField] public bool wallGrab;
    [SerializeField] private float groundRadius = 0.3f;
    [SerializeField] private float wallRadius = 0.3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] public LayerMask obstacleLayer;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public int jumpValueIter = 60;
    [SerializeField] public float speed = 2f;
    [SerializeField] public float climbSpeed = 4f;
    [SerializeField] private float slideSpeed = 1f;
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool isWalled;
    [SerializeField] private int maxAirDash = 1;
    [SerializeField] public float dashCooldown = 0.3f;
    [SerializeField] public int airDashesRemaining;
    [SerializeField] public float dashDistance = 5f;
    [SerializeField] public float dashDuration = 0.1f;
    [SerializeField] public float dashImpulse = 0.3f;
    [SerializeField] public bool isDashCooldown = false;

    public StateMachine movementSM;
    public IdleState idle;
    public RunningState run;
    public JumpingState jumping;


    public float gravityDef;
    private bool jumpControl;
    private int jumpIter = 0;
    private bool faceRight = true;
    public bool lockDash = false;
    private Coroutine dashCooldownCoroutine;

 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        gravityDef = rb.gravityScale;
        airDashesRemaining = maxAirDash;
        obstacleLayer = LayerMask.GetMask("Ground", "Wall");
        OnGroundTouch.AddListener(UpdateDash);
        movementSM = gameObject.AddComponent<StateMachine>();

        idle = new IdleState(this, movementSM);
        run = new RunningState(this, movementSM);
        jumping = new JumpingState(this, movementSM);

        movementSM.Initialize(idle);

    }
    private void Update()
    {
        Move(speed);
        Jump();
        OnGround();
        Reflect();
        OnWall();
        MoveOnWall();
        CheckGrounded();
        HandleDashInput();
        movementSM.CurrentState.HandleInput();

        movementSM.CurrentState.LogicUpdate();

    }
    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    public void Move(float speed)
    {
        if (wallGrab)
            return;
        moveVector.x = Input.GetAxis("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    void Reflect()
    {
        if((moveVector.x > 0 && !faceRight) || (moveVector.x < 0  && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }

    }


    public void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded) { jumpControl = true; }
        }
        else { jumpControl = false; }

        if (jumpControl && jumpForce > 0)
        {
            if(jumpIter++ < jumpValueIter)
            {
                rb.AddForce(Vector2.up * jumpForce / (jumpIter / 3f));
            }
        }
        else { jumpIter = 0; }
    }

    void OnGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
        if (!wasGrounded && isGrounded) { OnGroundTouch.Invoke(); }
    }
    void OnWall()
    {
        isWalled = Physics2D.OverlapCircle(wallCheck.position, wallRadius, wallMask);
    }

    public IEnumerator Dash(Vector2 direction)
    {
        lockDash = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        OnDash.Invoke();

        float dashTimeElapsed = 0f;
        Vector2 dashStartPosition = rb.position;
        while (dashTimeElapsed < dashDuration)
        {
            RaycastHit2D hit = Physics2D.Raycast(dashStartPosition, direction, dashDistance, obstacleLayer);
            if (hit.collider != null)
            {
                rb.position = hit.point - direction * 0.1f;
                break;
            }

            rb.MovePosition(rb.position + direction * dashImpulse * Time.fixedDeltaTime);
            dashTimeElapsed += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
            
        }
        rb.gravityScale = gravityDef;
        rb.velocity = Vector2.zero;
        Invoke("DashLock", dashCooldown);

    }
    public void DashLock()
    {
        lockDash = false;
    }
    
    public void CheckGrounded()
    {
        if (isGrounded && !isDashCooldown) { airDashesRemaining = maxAirDash; }
    }

    void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.C) && !lockDash && airDashesRemaining > 0)
        {
            Vector2 dashDirection = GetDashDirection();
            if (dashDirection != Vector2.zero)
            {
                StartCoroutine(Dash(dashDirection));
                airDashesRemaining--;  

                if (dashCooldownCoroutine != null)
                {
                    StopCoroutine(dashCooldownCoroutine);
                }
                dashCooldownCoroutine = StartCoroutine(DashCooldown());
            }
        }
    }

    public IEnumerator DashCooldown()
    {
        isDashCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashCooldown = false;
    }
    public Vector2 GetDashDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        return direction != Vector2.zero ? direction : (faceRight ? Vector2.right : Vector2.left);

    }

    void MoveOnWall()
    {
        if (isWalled && Input.GetKeyDown(KeyCode.X)) { wallGrab = true; }
        if (!isWalled || Input.GetKeyUp(KeyCode.X)) { wallGrab = false; }
        if (wallGrab)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            float verticalMove = Input.GetAxisRaw("Vertical");
            if (verticalMove != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalMove * climbSpeed);
            }
            else { rb.velocity = Vector2.zero; }
        }
        else if (!wallGrab && isWalled && Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.gravityScale = gravityDef;
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        }
        else
        {
            rb.gravityScale = gravityDef;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlatform")) {this.transform.parent = collision.transform;}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovingPlatform")) {this.transform.parent = null;}
    }
    public void UpdateDash()
    {
        airDashesRemaining = maxAirDash;
        if (dashCooldownCoroutine != null)
        {
            StopCoroutine(dashCooldownCoroutine);
        }
        dashCooldownCoroutine = StartCoroutine(DashCooldown());
        
    }
}
