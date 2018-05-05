using UnityEngine;
using System.Collections;
using System;

public class GameStateHandler : MonoBehaviour
{
    public StateMachine stateMachine;

    public void ChangeStateSmooth(GameState nextState)
    {
        StartCoroutine(WaitForFrame(() =>
        {
            Debug.Log(gameObject.name + ": GameState >> " + nextState + " -S- ");
            stateMachine.ChangeStateSmooth(nextState);
        }));

    }

    public void ChangeStateForced(GameState nextState)
    {
        StartCoroutine(WaitForFrame(() =>
        {
            Debug.Log(gameObject.name + ": GameState >> " + nextState + " -F- ");
            stateMachine.ChangeStateForced(nextState);
        }));
    }
    public void ReturnToPrevious(bool isForced)
    {
        if (isForced)
            ChangeStateForced(stateMachine.previousState);
        else
            ChangeStateSmooth(stateMachine.previousState);
    }

    public void ReloadGameState(bool isForced)
    {
        StartCoroutine(WaitForFrame(() =>
        {
            Debug.Log(gameObject.name + ": ReloadState >> " + stateMachine.currentState + " -" + (isForced ? 'F' : 'S') + "- ");
            stateMachine.ReloadCurrentState(isForced);
        }));
    }



    private IEnumerator WaitForFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        action.Invoke();
    }
}
