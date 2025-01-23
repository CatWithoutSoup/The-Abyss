using UnityEngine;

public class WallSlideState : State
{
    private bool isJumpingOffWall; 
    private bool jumpControl;     
    private int jumpIter;        
    private int jumpValueIter = 60; 
    private float jumpForce = 2f;  

    public WallSlideState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        isJumpingOffWall = false;
        jumpControl = false;
        jumpIter = 0;
        
    }
    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKey(KeyCode.Z))
        {
            if (player.isWalled)
            {
                jumpControl = true;
                
            }
        }
        else
        {
            jumpControl = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ((player.faceRight && Input.GetAxisRaw("Horizontal") < 0) || (!player.faceRight && Input.GetAxisRaw("Horizontal") > 0) || !player.isWalled && player.rb.velocity.y < 0) 
        { 
            stateMachine.ChangeState(player.fall);
        }
        else if (player.isGrounded) 
        {
            stateMachine.ChangeState(player.idle);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            stateMachine.ChangeState(player.grab);
        }

    }

    private void PerformWallJump()
    {
        isJumpingOffWall = true;
        if (jumpControl)
        {
            if (jumpIter++ < jumpValueIter)
            {
                float jumpDirection = player.faceRight ? -1 : 1; // Направление противоположное стене
                float horizontalForce = jumpForce * 2f;       // Горизонтальная составляющая
                float verticalForce = jumpForce / (jumpIter / 2f); // Вертикальная составляющая
                player.rb.velocity = new Vector2(jumpDirection * horizontalForce, verticalForce);
            }
        }
        else
        {
            jumpIter = 0;
            jumpControl = false;
            isJumpingOffWall = false;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (jumpControl && !isJumpingOffWall)
        {
            PerformWallJump();
        }
    }
    public override void Exit()
    {
        base.Exit();
        jumpIter = 0; 
        jumpControl = false; 
        isJumpingOffWall = false; 
        player.isWalled = false;
    }
}