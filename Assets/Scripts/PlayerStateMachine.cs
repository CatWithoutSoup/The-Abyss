using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour 
{
    PlayerState CurrentState;

    void Start()
    {
        CurrentState = GetInitialState();
        if (CurrentState != null)
            CurrentState.Enter();
    }

    //public void Initialize(PlayerState startingState)
    //{
    //    CurrentState = startingState;
    //    CurrentState.Enter();
    //}
    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    public void Update()
    {
        if (CurrentState != null)
            CurrentState.LogicUpdate();
        //CurrentState?.HandleInput();
        //CurrentState.LogicUpdate();
    }
    public void LateUpdate()
    {
        if (CurrentState != null)
            CurrentState.PhysicsUpdate();
        //CurrentState?.HandleInput();
        //CurrentState.LogicUpdate();
    }
    //public void FixedUpdate()
    //{
    //    CurrentState.PhysicsUpdate();
    //}
    protected virtual PlayerState GetInitialState()
    {
        return null;
    }
    private void OnGUI()
    {
        string content = CurrentState != null ? CurrentState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
