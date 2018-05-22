using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winMonitor : MonoBehaviour {

    public FloatVariable health;
    public GameEvent winEvent;

    public void CheckWin()
    {
        if (health.value <= 0)
        {
            winEvent.Raise();
        }
    }
}
