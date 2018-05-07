using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StateMachine", menuName = "Engine/StateMachine", order = 2)]
public class StateMachine : ScriptableObject
{

    public GameState previousState;
    public GameState currentState;

    public void ChangeStateForced(GameState newState, bool alowReloadCurrent = false)
    {
        if (!alowReloadCurrent && currentState == newState) { Debug.LogWarning("same state"); return; }

        if (currentState != null)
        {
            currentState.OnExit();
        }

        previousState = currentState;
        currentState = newState;
        Debug.LogWarning("state changed:" + currentState);
        currentState.OnEnter();
    }

    internal void Init()
    {
        previousState = null;
        currentState = null;
    }


    /// <summary>
    /// Chnage state only if all listener is done and ready to change state
    /// Also it set the current listener to Done if failed to change now
    /// returns True if success
    /// returns false if failed
    /// </summary>
    /// <param name="newState"></param>
    /// <returns></returns>
    public bool ChangeStateSmooth(GameState newState, bool alowReloadCurrent = false)
    {
        if (!alowReloadCurrent && currentState == newState) { Debug.LogWarning("same state"); return false; }

        if (currentState != null)
        {
            if (currentState.IsAllListenersDone())
            {
                ChangeStateForced(newState);
                return true;//success
            }
            else
            {
                Debug.LogWarning("IsAllListenersDone: false");
                return false;//fail
            }
        }
        else
        {
            ChangeStateForced(newState); //no current state so change anyway
            return true;
        }

    }

    internal void ReloadCurrentState(bool isForced)
    {
        if (currentState != null)
        {
            if (isForced)
                ChangeStateForced(currentState);
            else
                ChangeStateSmooth(currentState);
        }
        else
        {
            Debug.LogError("Cant reload: Current state : NULL");
        }
    }
}