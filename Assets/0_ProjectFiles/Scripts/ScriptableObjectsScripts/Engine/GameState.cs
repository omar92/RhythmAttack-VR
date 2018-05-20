using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Engine/GameState", order = 2)]
public class GameState : ScriptableObject
{
    private List<StateChangeListener> Listeners = new List<StateChangeListener>();

    public void OnEnter()
    {
        //Debug.Log(Listeners.Count);
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnEnter(this);
        }
    }

    public void OnPause()
    {
        Debug.Log(Listeners.Count);
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnPause(this);
        }
    }

    public void OnUnPause()
    {
        Debug.Log(Listeners.Count);
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners[i].OnUnPause(this);
        }
    }

    public void OnExit()
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
        //    Debug.Log(Listeners[i].gameObject.name + " " + this + " OnExit");
            Listeners[i].OnExit(this);
        }
    }

    //internal void SetDone(StateChangeListener listener, bool isDone)
    //{
    //    if (Listeners.Contains(listener))
    //    {
    //        //Debug.Log("listener of " + listener.gameObject.name + " isDone: "+ isDone);
    //        Listeners[listener] = isDone;
    //    }
    //    else
    //        Debug.LogError("listener of " + listener.gameObject.name + " not found");
    //}

    public void RegisterListener(StateChangeListener listener)
    {
        if (!Listeners.Contains(listener)) Listeners.Add(listener);
        else
            Debug.LogError("listener of " + listener.gameObject.name + " already exiist");
    }

    public void UnregisterListener(StateChangeListener listener)
    {
        if (Listeners.Contains(listener)) Listeners.Remove(listener);
    }

    //internal bool IsAllListenersDone()
    //{
    //    for (int i = Listeners.Count - 1; i >= 0; i--)
    //    {
    //        if (Listeners.Values.ElementAt(i) == false)
    //        {
    //            Debug.LogWarning(Listeners.Keys.ElementAt(i).name + " not ready");
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
