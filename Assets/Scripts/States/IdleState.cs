using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : Grounded
{
    private float _horizontalInput;
    public IdleState(PlayerMovement stateMachine) : base("IdleState", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _horizontalInput = 0f;
        //player.rb.velocity = Vector2.zero;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
            stateMachine.ChangeState(((PlayerMovement) stateMachine).runningState);
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
