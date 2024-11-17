using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FallingState : State
{
    public FallingState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.isGrounded)
            stateMachine.ChangeState(player.idle);
        else if (Input.GetKey(KeyCode.C))
            stateMachine.ChangeState(player.dash);
        else if (Input.GetKey(KeyCode.X))
            stateMachine.ChangeState(player.grab);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.anim.Play("Fall");
        float horizontalInput = Input.GetAxis("Horizontal");
        player.rb.velocity = new Vector2(horizontalInput * player.speed, player.rb.velocity.y);
        player.Reflect(horizontalInput);
        //if ((horizontalInput > 0 && !player.faceRight) || (horizontalInput < 0 && player.faceRight))
        //{
        //    player.transform.localScale *= new Vector2(-1, 1);
        //    player.faceRight = !player.faceRight;
        //}
        //player.Reflect();
        //player.rb.gravityScale = player.gravityDef;
    }
}
