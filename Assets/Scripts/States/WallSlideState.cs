using UnityEngine;

public class WallSlideState : State
{
    private bool isJumpingOffWall; // Флаг для отслеживания прыжка
    private bool jumpControl;     // Контроль прыжка (удержание кнопки)
    private int jumpIter;         // Итерации для контролируемого прыжка
    private int jumpValueIter = 60; // Максимальное количество итераций прыжка
    private float jumpForce = 2f;  // Базовая сила прыжка

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

        // Проверка удержания кнопки прыжка
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
        //else if (Input.GetKey(KeyCode.Z))
        //{
        //    stateMachine.ChangeState(player.wallJump);
        //}


    }

    private void PerformWallJump()
    {
        isJumpingOffWall = true;
        if (jumpControl)
        {
            // Логика контролируемого прыжка
            if (jumpIter++ < jumpValueIter)
            {
                // Направление прыжка от стены (диагональный прыжок)
                float jumpDirection = player.faceRight ? -1 : 1; // Направление противоположное стене
                float horizontalForce = jumpForce * 1f;       // Горизонтальная составляющая
                float verticalForce = jumpForce / (jumpIter / 3f); // Вертикальная составляющая
                player.rb.velocity = new Vector2(jumpDirection * horizontalForce, verticalForce);
            }
        }
        else
        {
            jumpIter = 0;
            jumpControl = false;
            isJumpingOffWall = false;

            // Завершаем прыжок, переходя в состояние прыжка
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
        jumpIter = 0; // Сбрасываем итерации
        jumpControl = false; // Сбрасываем контроль прыжка
        isJumpingOffWall = false; // Сбрасываем флаг прыжка
        player.isWalled = false;
    }
}