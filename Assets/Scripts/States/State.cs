using UnityEngine;

public abstract class State 
{
    protected PlayerMovement player;
    protected StateMachine stateMachine;
    protected State(PlayerMovement player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}
