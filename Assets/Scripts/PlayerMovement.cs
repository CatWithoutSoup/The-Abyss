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
    public Animator anim;
    public float boxCastDistanceGround = 0.1f;
    public float boxWidthGround = 0.45f;
    public float boxHeightGround = 0.15f;
    public float boxCastDistanceWall = 0.1f;
    public float boxWidthWall = 0.15f;
    public float boxHeightWall = 0.15f;

    [HideInInspector] public bool isWallJumping;
    //public Vector2 wallJumpDirection = new Vector2(1, 1); // Направление прыжка
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
    [SerializeField] public float dashDistance = 15f;
    [SerializeField] public float dashDuration = 0.3f;
    [SerializeField] public float dashImpulse = 0.3f;
    [SerializeField] public bool isDashCooldown = false;
    [SerializeField] public int jumpCount = 1;
    [SerializeField] public float wallJumpHorizontalForce = 1.5f;
    [Header("WallJump")]
    [SerializeField] public float jumpHoldTime = 0f;
    public float verticalWallJumpForce = 5f;
    public float wallJumpForce = 5f; // Сила прыжка от стены
    public float maxJumpHoldTime = 0.2f;
    private bool isHoldingWall;

    public StateMachine movementSM;
    public IdleState idle;
    public RunningState run;
    public JumpingState jumping;
    public DashingState dash;
    public FallingState fall;
    public WallClimbState climb;
    public WallGrabState grab;
    public WallSlideState slide;
    public WallJumpState wallJump;
    public CanvasGroup visibleStaminaBar;

    public float gravityDef;
    private bool jumpControl;
    private int jumpIter = 0;
    public bool faceRight = true;
    public bool lockDash = false;
    private Coroutine dashCooldownCoroutine;
    public Stamina stamina;
    //private StaminaBarUI can;  


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //can = GetComponent<StaminaBarUI>();  
    }

    private void Start()
    {
        gravityDef = rb.gravityScale;
        airDashesRemaining = maxAirDash;
        obstacleLayer = LayerMask.GetMask("Ground", "Wall");
        OnGroundTouch.AddListener(UpdateDash);
        movementSM = gameObject.AddComponent<StateMachine>();
        stamina = gameObject.GetComponent<Stamina>();

        idle = new IdleState(this, movementSM);
        run = new RunningState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        dash = new DashingState(this, movementSM);
        fall = new FallingState(this, movementSM);
        climb = new WallClimbState(this, movementSM);
        grab = new WallGrabState(this, movementSM);
        slide = new WallSlideState(this, movementSM);
        wallJump = new WallJumpState(this, movementSM);

        movementSM.Initialize(idle);

    }
    private void Update()
    {
        //Move();
        //Jump();
        OnGround();
        //Reflect();
        OnWall();
        MoveOnWall();
        //CheckGrounded();
        HandleDashInput();
        //WallJump();
        movementSM.CurrentState.HandleInput();

        movementSM.CurrentState.LogicUpdate();
        //if (isHoldingWall && Input.GetKeyDown(KeyCode.Z)) // Прыжок от стены
        //{
        //    WallJump();
        //}
    }
    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    public void Move()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    public void Reflect(float horizontalInput)
    {
        //can.visibleStaminaBar.transform.localScale = new Vector3(1, 1, 1);
        if((horizontalInput > 0 && !faceRight) || (horizontalInput < 0  && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }

    }


    public void Jump()
    {
        if (Input.GetKey(KeyCode.Z))
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
        isGrounded = Physics2D.BoxCast(
            groundCheck.position,
            new Vector2(boxWidthGround, boxHeightGround),
            0f,
            Vector2.down,
            boxCastDistanceGround,
            groundMask
        );
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
        anim.SetBool("OnGround", isGrounded);
        if (!wasGrounded && isGrounded) { OnGroundTouch.Invoke(); }
    }
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            // Отрисовка области проверки земли
            Gizmos.DrawWireCube(
                groundCheck.position + Vector3.down * boxCastDistanceGround / 2,
                new Vector3(boxWidthGround, boxHeightGround, 0)
            );
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;

            // Отрисовка области проверки стены
            Gizmos.DrawWireCube(
                wallCheck.position + Vector3.down * boxCastDistanceWall / 2,
                new Vector3(boxWidthWall, boxHeightWall, 0)
            );
        }
    }
    void OnWall()
    {
        isWalled = Physics2D.BoxCast(
            wallCheck.position,
            new Vector2(boxWidthWall, boxHeightWall),
            0f,
            Vector2.down,
            boxCastDistanceWall,
            groundMask);
        anim.SetBool("IsWalled", isWalled);
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
                float remainingDistance = Vector2.Distance(rb.position, hit.point - direction * 0.1f);
                if (hit.collider.CompareTag("Platform"))
                {
                    dashTimeElapsed += Time.fixedDeltaTime;
                    rb.MovePosition(rb.position + direction * dashImpulse * Time.fixedDeltaTime);
                    yield return new WaitForFixedUpdate();
                    continue;
                }
                else if (remainingDistance <= dashImpulse * Time.fixedDeltaTime)
                {
                    rb.MovePosition(hit.point - direction * 0.1f);
                    break;
                }
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

    public void HandleDashInput()
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

    public void MoveOnWall()
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
        isDashCooldown = false;
    }
    public void AddExtraJumps()
    {

    }
    //void WallJump()
    //{
    //    isWallJumping = true;
    //    isHoldingWall = false; // Отпускаем стену

    //    jumpControl = true;

    //    rb.velocity = Vector2.zero; // Сброс скорости
    //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Начальный импульс

    //    StartCoroutine(ControlWallJump());
    //}

    //private IEnumerator ControlWallJump()
    //{
    //    float jumpDuration = 0f;

    //    while (Input.GetKey(KeyCode.Z) && jumpDuration < jumpValueIter)
    //    {
    //        rb.AddForce(Vector2.up * jumpForce / 3f); // Контроль высоты прыжка
    //        jumpDuration += Time.deltaTime;
    //        yield return null;
    //    }

    //    jumpIter = 0;
    //    isWallJumping = false;

    //    if (Input.GetKey(KeyCode.X)) // Если удерживаем кнопку захвата
    //    {
    //        isHoldingWall = true;
    //    }
    //}
}
