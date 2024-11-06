using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class JumpingState : State
{
    private bool _grounded;
    public JumpingState(PlayerMovement player, StateMachine stateMachine) : base(player, stateMachine) 
    {
    }

    public override void Enter()
    {
        base.Enter();
        _grounded = false;
        player.Jump();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_grounded)
            stateMachine.ChangeState(player.idle);
    }
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _grounded = player.isGrounded;
    }
}
