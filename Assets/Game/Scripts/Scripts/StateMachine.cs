using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    IState currentState;
    IState previousState;

    GameManager gameManager;

    public StateMachine(GameManager gameManager) { this.gameManager = gameManager; }

    public void ChangeState(IState newState)
    {
       // Debug.Log("Change state" + newState);
        if (currentState != null)
        {
            currentState.Exit();
        }
        previousState = currentState;
        currentState = newState;
        currentState.Enter(this, gameManager);
    }
    public void StateUpdate()
    {
        if (currentState != null)
        {
            currentState.Excute();
        }
    }
    public void SwitchToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter(this, gameManager);
    }
}
