using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        //Debug.Log(currentState.GetType().Name + " -> " + newState.GetType().Name);
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public string GetState() {
        return currentState.GetType().Name;
    }
}
