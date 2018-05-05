using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Engine/GameState", order = 2)]
public class GameState : ScriptableObject
{
    private Dictionary<StateChangeListener, bool> Listeners = new Dictionary<StateChangeListener, bool>();

    public void OnEnter()
    {
        Debug.Log(Listeners.Count);
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners.Keys.ElementAt(i).OnEnter(this);
        }
    }
    public void OnExit()
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            Listeners.Keys.ElementAt(i).OnExit(this);
        }
    }

    internal void SetDone(StateChangeListener listener, bool isDone)
    {
        if (Listeners.Keys.Contains(listener))
        {
            //Debug.Log("listener of " + listener.gameObject.name + " isDone: "+ isDone);
            Listeners[listener] = isDone;
        }
        else
            Debug.LogError("listener of " + listener.gameObject.name + " not found");
    }

    public void RegisterListener(StateChangeListener listener)
    {
        if (!Listeners.Keys.Contains(listener)) Listeners.Add(listener, false);
        else
            Debug.LogError("listener of " + listener.gameObject.name + " already exiist");
    }

    internal bool IsAllListenersDone()
    {
        for (int i = Listeners.Count - 1; i >= 0; i--)
        {
            if (Listeners.Values.ElementAt(i) == false)
            {
                Debug.LogWarning(Listeners.Keys.ElementAt(i).name + " not ready");
                return false;
            }
        }
        return true;
    }

    public void UnregisterListener(StateChangeListener listener)
    {
        if (Listeners.Keys.Contains(listener)) Listeners.Remove(listener);
    }
}
