using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FallingState : State
{
    public FallingState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.anim.Play("Fall");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.isGrounded)
            stateMachine.ChangeState(player.idle);
        else if (player.isGrounded && Input.GetAxisRaw("Horizontal") != 0)
            stateMachine.ChangeState(player.run);
        else if (Input.GetKey(KeyCode.C))
            stateMachine.ChangeState(player.dash);
        else if (Input.GetKey(KeyCode.X))
            stateMachine.ChangeState(player.grab);
        else if (player.isWalled && player.rb.velocity.y < 0)
            stateMachine.ChangeState(player.slide);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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
