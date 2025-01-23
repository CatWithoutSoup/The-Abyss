using UnityEngine;

public class WallGrabState : State
{
    private bool jumpControl;
    private int jumpIter;
    private float jumpForceVertical = 3f;
    private int jumpValueIterVertical = 60;
    public WallGrabState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        jumpControl = false;
        jumpIter = 0;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.isWalled && player.rb.velocity.y < 0 && !Input.GetKey(KeyCode.X))
        {
            stateMachine.ChangeState(player.slide);
        }
        if (!player.isWalled || Input.GetKeyUp(KeyCode.X))
        {
            stateMachine.ChangeState(player.fall);  
        }
        else if (Input.GetKey(KeyCode.X) && player.rb.velocity.y != 0)
        {
            stateMachine.ChangeState(player.climb);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            jumpIter = 0;
            jumpControl = true;
            PerformWallClimbJump();
        }
    }
    private void PerformWallClimbJump()
    {
        if (jumpControl)
        {
            // Логика контролируемого прыжка строго вертикально вверх
            if (jumpIter++ < jumpValueIterVertical)
            {
                float verticalForce = jumpForceVertical / (jumpIter / 2f); // Вертикальная составляющая
                player.rb.velocity = new Vector2(0, verticalForce);

            }
            else
            {
                // Сбрасываем итерации, флаги и завершаем прыжок
                jumpIter = 0;
                jumpControl = false;
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
