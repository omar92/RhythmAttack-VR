using UnityEngine;
using System.Collections;

public class GameStateHandler : MonoBehaviour
{
    public StateMachine stateMachine;

    public void ChangeStateSmooth(GameState nextState)
    {
        Debug.Log(gameObject.name + ": GameState >> " + nextState + " -S- ");
        stateMachine.ChangeStateSmooth(nextState);
    }
    public void ChangeStateForced(GameState nextState)
    {
        Debug.Log(gameObject.name + ": GameState >> " + nextState + " -F- ");
        stateMachine.ChangeStateForced(nextState);
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
        if (isForced)
            ChangeStateForced(stateMachine.currentState);
        else
            ChangeStateSmooth(stateMachine.currentState);

    }
}
