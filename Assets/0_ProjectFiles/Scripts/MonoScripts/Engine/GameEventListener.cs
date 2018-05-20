using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    Coroutine co;

    internal void OnEnable()
    {
        Event.RegisterListener(this);
    }
    internal void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    internal void OnEventRaised()
    {
       // if (co == null)
            co = StartCoroutine(WaitForEndOfFrame());
    }

    private IEnumerator WaitForEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        Response.Invoke();
      //  co = null;
    }
}
