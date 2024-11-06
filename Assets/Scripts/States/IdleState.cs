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
        //player.rb.velocity = Vector2.zero;
    }
    public override void HandleInput()
    {
        base.HandleInput();
        dash = Input.GetKey(KeyCode.C);
        jump = Input.GetKey(KeyCode.Space);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //if (dash)
        //{
        //    stateMachine.ChangeState(player.);
        //}
        if (jump)
        {
            stateMachine.ChangeState(player.jumping);
        }
        else if (run)
        {
            stateMachine.ChangeState(player.run);
        }
        //_horizontalInput = Input.GetAxis("Horizontal");
        //if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
        //    stateMachine.ChangeState(((PlayerMovement) stateMachine).runningState);
    }

    //public override void PhysicsUpdate()
    //{
    //    //if (player.moveVector.x != 0)
    //    //    stateMachine.ChangeState(player.runningState);
    //    //else if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
    //    //    stateMachine.ChangeState(player.jumpingState);
    //    //else if (!player.isGrounded)
    //    //    stateMachine.ChangeState(player.fallingState);
    //}
}
