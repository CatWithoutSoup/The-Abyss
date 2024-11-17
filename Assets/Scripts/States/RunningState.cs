using Unity.VisualScripting;
using UnityEngine;


public class RunningState : Grounded
{
    public RunningState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Input.GetAxis("Horizontal") == 0)
            stateMachine.ChangeState(player.idle);
        else if (Input.GetKey(KeyCode.Z))
            stateMachine.ChangeState(player.jumping);
        else if (Input.GetKey(KeyCode.X))
            stateMachine.ChangeState(player.grab);
        else if (Input.GetKey(KeyCode.C))
            stateMachine.ChangeState(player.dash);

    }
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.anim.SetFloat("moveX", Mathf.Abs(player.rb.velocity.x));
        player.Move();
        player.Reflect(Input.GetAxis("Horizontal"));
        
    }

}
