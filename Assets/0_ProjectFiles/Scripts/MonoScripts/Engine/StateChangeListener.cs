using UnityEngine;
using UnityEngine.Events;

public class StateChangeListener : MonoBehaviour
{
    public StatesHandling[] states;


    internal void OnEnter(GameState state)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].state.GetInstanceID() == state.GetInstanceID())
            {
                Debug.Log(states[i].state +": "+gameObject.name+ ": OnEnter invoke( ) "+ states[i].onEnter);
                states[i].onEnter.Invoke();
            }
        }
    }

    internal void OnPause(GameState state)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].state.GetInstanceID() == state.GetInstanceID())
                states[i].onPause.Invoke();
        }
    }

    internal void OnUnPause(GameState state)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].state.GetInstanceID() == state.GetInstanceID())
                states[i].onUnPause.Invoke();
        }
    }

    internal void OnExit(GameState state)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].state.GetInstanceID() == state.GetInstanceID())
                states[i].onExit.Invoke();
        }
    }

    internal void OnEnable()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i].state.RegisterListener(this);
            if (!states[i].MustBeDone)
            {
                SetDone(states[i].state);
            }
        }
    }

    internal void OnDisable()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i].state.UnregisterListener(this);
        }

    }

    public void SetDone(GameState state)
    {
        state.SetDone(this, true);
    }
    public void SetUnDone(GameState state)
    {
        state.SetDone(this, false);
    }

}
[System.Serializable]
public struct StatesHandling
{
    public GameState state;
    /// <summary>
    /// Can this listener change state before Done
    /// </summary>
    public bool MustBeDone;
    public UnityEvent onEnter;
    public UnityEvent onPause;
    public UnityEvent onUnPause;
    public UnityEvent onExit;
}
