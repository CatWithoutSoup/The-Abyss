using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class Grounded : State
{
    protected float speed;
    public Grounded(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) 
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.OnRun.Invoke(); // Вызов анимации или звуков для бега
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //// Получение горизонтального ввода для управления скоростью персонажа
        //float horizontalInput = Input.GetAxis("Horizontal");
        //player.moveVector.x = horizontalInput * player.speed;

        //// Условия для смены состояния
        //if (horizontalInput == 0)
        //{
        //    stateMachine.ChangeState(player.idleState); // Переход в IdleState при остановке
        //}
        //else if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        //{
        //    stateMachine.ChangeState(player.jumpingState); // Переход в JumpingState при прыжке
        //}
        //else if (Input.GetKeyDown(KeyCode.C) && player.airDashesRemaining > 0 && !player.isDashCooldown)
        //{
        //    stateMachine.ChangeState(player.dashingState); // Переход в DashingState при нажатии на рывок
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleInput()
    {
        base.HandleInput();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
}
