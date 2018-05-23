using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PauseGame : MonoBehaviour
{

    public GameEvent pauseEvent;
    
   
    void Start()
    {
        OVRManager.HMDLost += HandleHMDLost;
        //OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;
    }
    void Update()
    {
        ApplicationNotFocused();
    }
    

    void HandleHMDUnmounted()
    {
        pauseEvent.Raise();
    }

    void HandleHMDLost()
    {
        pauseEvent.Raise();

    }
    void ApplicationNotFocused()
    {
        if (!Application.isFocused)
        {
            pauseEvent.Raise();
        }
           
    }
}
