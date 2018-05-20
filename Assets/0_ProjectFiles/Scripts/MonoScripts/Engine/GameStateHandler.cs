using UnityEngine;
using System.Collections;
using System;

public class GameStateHandler : MonoBehaviour
{
    public StateMachine stateMachine;
    Coroutine co;
    public void ChangeStateSmooth(GameState nextState)
    {
      //  if (co == null)
            co = StartCoroutine(WaitForFrame(() =>
            {
              //  Debug.Log(gameObject.name + ": GameState >> " + nextState + " -S- ");
                stateMachine.ChangeStateSmooth(nextState);
           //     co = null;
            }));

    }

    public void ChangeStateForced(GameState nextState)
    {
       // if (co == null)
            co = StartCoroutine(WaitForFrame(() =>
           {
           //    Debug.Log(gameObject.name + ": GameState >> " + nextState + " -F- ");
               stateMachine.ChangeStateForced(nextState);
          //     co = null;
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
      //  if (co == null)
            co = StartCoroutine(WaitForFrame(() =>
          {
            //  Debug.Log(gameObject.name + ": ReloadState >> " + stateMachine.currentState + " -" + (isForced ? 'F' : 'S') + "- ");
              stateMachine.ReloadCurrentState(isForced);
          //    co = null;
          }));
    }



    private IEnumerator WaitForFrame(Action action)
    {
        yield return new WaitForEndOfFrame();
        action.Invoke();
    }
}
