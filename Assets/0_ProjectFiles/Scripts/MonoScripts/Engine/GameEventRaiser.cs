using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiser : MonoBehaviour
{
    public void RaiseGameEvent(GameEvent gameEvent)
    {
        gameEvent.Raise();
    }
}
