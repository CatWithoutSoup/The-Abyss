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
            stateMachine.ChangeState(player.fall);  
        else if (player.rb.velocity.y == 0)
            stateMachine.ChangeState(player.grab);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.MoveOnWall();
    }
}
