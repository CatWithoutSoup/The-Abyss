using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class JumpingState : State
{
    public JumpingState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.rb.velocity.y < 0 && player.isWalled)
        {
            stateMachine.ChangeState(player.slide);
        }
        else if (player.rb.velocity.y < 0 && !player.isGrounded)
        {
            stateMachine.ChangeState(player.fall);
        }
        else if (Input.GetKey(KeyCode.C))
            stateMachine.ChangeState(player.dash);
        else if (Input.GetKey(KeyCode.X) && player.isWalled)
            stateMachine.ChangeState(player.grab);
        
    }
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();      
        player.rb.velocity = new Vector2(Input.GetAxis("Horizontal") * player.speed, player.rb.velocity.y);
        player.Jump();
        player.Reflect(Input.GetAxis("Horizontal"));
    }
}
