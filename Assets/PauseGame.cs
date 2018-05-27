using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PauseGame : MonoBehaviour
{

    public GameEvent pauseEvent;
    bool firstWear =false;
   
    void Start()
    {
        OVRManager.HMDLost += HandleHMDLost;
        OVRManager.HMDMounted += FirstWearSetDirty;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;
    }
    void Update()
    {
        ApplicationNotFocused();
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            pauseEvent.Raise();
        }
        if (OVRInput.Get(OVRInput.Button.Back))
        {
            pauseEvent.Raise();
        }
    }
    

    void HandleHMDUnmounted()
    {
        if (firstWear)
        {
            pauseEvent.Raise();
            firstWear = false;
        }
        
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
    void FirstWearSetDirty()
    {
        firstWear = true ;
    }
}
