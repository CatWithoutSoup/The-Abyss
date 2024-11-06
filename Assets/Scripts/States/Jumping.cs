using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Jumping : PlayerState
{
    private PlayerMovement _pm;
    private bool _grounded;
    private int _groundLayer = 1 << 6;
    public Jumping(PlayerMovement stateMachine) : base("Jumping", stateMachine) 
    {
        _pm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        Vector2 vel = _pm.rb.velocity;
        vel.y += _pm.jumpForce;
        _pm.rb.velocity = vel;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_grounded)
            stateMachine.ChangeState(_pm.idleState);
    }
    

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _grounded = _pm.rb.velocity.y < Mathf.Epsilon && _pm.rb.IsTouchingLayers(_groundLayer);
    }
}
