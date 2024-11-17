using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClimbState : State
{
    public WallClimbState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!player.isWalled || Input.GetKeyUp(KeyCode.X))
            stateMachine.ChangeState(player.fall);  // Если игрок отпускает кнопку, то переключаемся на падение
        else if (player.rb.velocity.y == 0)
            stateMachine.ChangeState(player.grab);



        //float verticalMove = Input.GetAxisRaw("Vertical");
        //player.rb.velocity = new Vector2(player.rb.velocity.x, verticalMove * player.climbSpeed);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.MoveOnWall();
    }
}
