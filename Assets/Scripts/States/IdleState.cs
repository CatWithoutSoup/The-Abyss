using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IdleState : Grounded
{
    private float _horizontalInput;
    private bool jump;
    private bool dash;
    private bool run;
    public IdleState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        jump = false;
        //dash = false;
        run = false;
        player.rb.velocity = Vector2.zero;

    }
    public override void HandleInput()
    {
        base.HandleInput();
        dash = Input.GetKey(KeyCode.C);
        jump = Input.GetKey(KeyCode.Z);
        if (Input.GetAxis("Horizontal") != 0)
            run = true;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (jump)
            stateMachine.ChangeState(player.jumping);
        else if (run)
            stateMachine.ChangeState(player.run);
        else if (dash)
            stateMachine.ChangeState(player.dash);
        else if (player.rb.velocity.y < 0 && !player.isGrounded)
            stateMachine.ChangeState(player.fall);
        else if (Input.GetAxis("Vertical") != 0 && player.isWalled)
            stateMachine.ChangeState(player.climb);
        
        //_horizontalInput = Input.GetAxis("Horizontal");
        //if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        //    stateMachine.ChangeState(((PlayerMovement) stateMachine).runningState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.anim.SetFloat("moveX", 0f);
        player.rb.velocity = Vector2.zero;
    }
}
