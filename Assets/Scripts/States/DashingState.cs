using UnityEngine;


public class DashingState : State
{

    public DashingState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.anim.SetBool("IsDash", true);
        player.anim.Play("Dash");
        player.OnDash.Invoke(); // Запуск анимации или эффекта рывка

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetAxis("Horizontal") == 0 && player.isGrounded)
            stateMachine.ChangeState(player.idle);
        else if (player.rb.velocity.y < 0)
            stateMachine.ChangeState(player.fall);
        else if (Input.GetKey(KeyCode.X))
            stateMachine.ChangeState(player.grab);

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.CheckGrounded();
        //player.HandleDashInput();
    }
    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("IsDash", false);
    }

}
