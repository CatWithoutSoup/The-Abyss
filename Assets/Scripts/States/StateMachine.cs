using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour 
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }
    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
    private void OnGUI()
    {
        if (CurrentState != null)
        {
            GUILayout.BeginArea(new Rect(10, 10, 200, 50));
            GUILayout.Label(CurrentState.ToString());
            GUILayout.EndArea();


        }
    }
}
