using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent OnGroundTouch;
    [SerializeField] public UnityEvent OnDash;
    [SerializeField] private UnityEvent OnWallGrabStart;
    [SerializeField] private UnityEvent OnWallGrabEnd;
    [Header("Components")]
    public Rigidbody2D rb; 
    private SpriteRenderer sr;
    public Vector2 moveVector;
    public Animator anim;
    private Stamina stamina;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] public LayerMask obstacleLayer;
    public AudioSource audioSource;
    [Header("Particle")]
    [SerializeField] GameObject Dust;
    [SerializeField] GameObject SlideParticle;
    [SerializeField] GameObject DashTrail;
    [SerializeField] Transform DashTrailSpawn;
    [Header("Gizmos")]
    [SerializeField] private float boxCastDistanceGround = 0.1f;
    [SerializeField] private float boxWidthGround = 0.45f;
    [SerializeField] private float boxHeightGround = 0.15f;
    [SerializeField] private float boxCastDistanceWall = 0.1f;
    [SerializeField] private float boxWidthWall = 0.15f;
    [SerializeField] private float boxHeightWall = 0.15f;
    [Header("Dash")]
    [SerializeField] private int maxAirDash = 1;
    [SerializeField] public float dashCooldown = 0.3f;
    [SerializeField] public int airDashesRemaining;
    [SerializeField] public float dashDistance = 15f;
    [SerializeField] public float dashDuration = 0.3f;
    [SerializeField] public float dashImpulse = 0.3f;
    [SerializeField] public bool isDashCooldown = false;
    [SerializeField] public bool lockDash = false;
    [Header("Jump")]
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public int jumpValueIter = 20;
    private bool jumpControl;
    private int jumpIter = 0;
    [Header("Wall")]
    [SerializeField] public bool wallGrab;
    [SerializeField] public float climbSpeed = 4f;
    [SerializeField] private float slideSpeed = 1f;
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool isWalled;
    [HideInInspector] public bool isWallJumping;
    [Header("Constant")]
    public float gravityDef;
    [SerializeField] public float speed = 2f;
    public bool faceRight = true;
    private Coroutine dashCooldownCoroutine;
    [Header("StateMachine")]
    public StateMachine movementSM;
    public IdleState idle;
    public RunningState run;
    public JumpingState jumping;
    public DashingState dash;
    public FallingState fall;
    public WallClimbState climb;
    public WallGrabState grab;
    public WallSlideState slide;
    public CanvasGroup visibleStaminaBar;
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
        audioSource = GetComponent<AudioSource>();
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

        movementSM.Initialize(idle);

    }
    private void Update()
    {

        OnGround();
        OnWall();
        MoveOnWall();
        HandleDashInput();
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        Jump();
        movementSM.CurrentState.PhysicsUpdate();
    }

    public void Move()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    public void Reflect(float horizontalInput)
    {
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
        anim.SetBool("OnGround", isGrounded);
        if (!wasGrounded && isGrounded) 
        { 
            OnGroundTouch.Invoke();
            Debug.Log("Эффект");
            Instantiate(Dust, transform.position, Dust.transform.rotation);
        }
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
            Instantiate(DashTrail, DashTrailSpawn.position, Quaternion.identity);
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
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right * (faceRight ? 1 : -1), 0.1f);
        if (isWalled && Input.GetKeyDown(KeyCode.X) && !wallHit.collider.CompareTag("Platform"))
        { 
            wallGrab = true;
            anim.SetBool("WallGrab", wallGrab);
        }
        if (!isWalled || Input.GetKeyUp(KeyCode.X))
        { 
            wallGrab = false;
            anim.SetBool("WallGrab", wallGrab);
        }
        if (wallGrab)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            float verticalMove = Input.GetAxisRaw("Vertical");
            anim.SetBool("moveY", false);
            if (verticalMove != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalMove * climbSpeed);
                anim.SetBool("moveY", true);

            }
            else { rb.velocity = Vector2.zero; }
        }
        else if (!wallGrab && isWalled && Input.GetAxisRaw("Horizontal") != 0 && !isGrounded)
        {
            rb.gravityScale = gravityDef;
            rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
            Instantiate(SlideParticle, transform.position, SlideParticle.transform.rotation);
        }
        else if (wallGrab && Input.GetKey(KeyCode.Z) && stamina.currentStamina > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.gravityScale = gravityDef;
        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name.Equals("Moving Platform")) {this.transform.parent = collision.transform;}
    //    if (collision.gameObject.name.Equals("TouchToMove")) { this.transform.parent = collision.transform; }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name.Equals("Moving Platform")) {this.transform.parent = null;}
    //    if (collision.gameObject.name.Equals("TouchToMove")) { this.transform.parent = null; }

    //}
    public void UpdateDash()
    {
        airDashesRemaining = maxAirDash;
        isDashCooldown = false;
    }
    public void AddExtraJumps()
    {

    }
}
