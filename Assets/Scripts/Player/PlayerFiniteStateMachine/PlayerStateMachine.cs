using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        Debug.Log(currentState.GetType().Name + " -> " + newState.GetType().Name);
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
