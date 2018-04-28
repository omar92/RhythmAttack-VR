using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    IState currentState;
    IState previousState;

	public void changeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }
    public void stateUpdate()
    {
        var runningState = currentState;
        if (runningState != null)
        {
            runningState.Excute();
        }
    }
    public void switchToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }
}
