using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEventsHandler : MonoBehaviour
{

    public UnityEvent OnAwake;
    public UnityEvent OnStart;
    public UnityEvent OnUpdate;

    private void Awake()
    {
        OnAwake.Invoke();
    }
    void Start()
    {
        OnStart.Invoke();
    }
    void Update()
    {
        OnUpdate.Invoke();
    }
}
