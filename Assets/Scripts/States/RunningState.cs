using Unity.VisualScripting;
using UnityEngine;


public class RunningState : Grounded
{
    private float _horizontalInput;
    public RunningState(PlayerMovement stateMachine) : base("RunningState", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        //player.OnRun.Invoke(); // Вызов анимации или звуков для бега
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
            stateMachine.ChangeState(_pm.idleState);

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
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector2 vel = _pm.rb.velocity;
        vel.x = _horizontalInput * _pm.speed;
        _pm.rb.velocity = vel;
        //player.rb.velocity = new Vector2(player.moveVector.x, player.rb.velocity.y); // Обновление скорости по горизонтали
    }

    //public override void Exit()
    //{
    //    base.Exit();
    //    //player.OnRunEnd.Invoke(); // Остановка анимации бега, если необходимо
    //}
}
