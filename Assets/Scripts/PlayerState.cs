using UnityEngine;

public abstract class PlayerState 
{
    protected PlayerMovement player;
    protected PlayerStateMachine stateMachine;
    public string name;

    public PlayerState(string name, PlayerStateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void HandleInput() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}
