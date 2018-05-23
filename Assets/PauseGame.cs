using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PauseGame : MonoBehaviour
{

    

   
    void Start()
    {
        OVRManager.HMDLost += HandleHMDLost;
        OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;
    }
    void Update()
    {

        //Debug.Log("pause the game");
        //if (false )
        //{
        //    pauseEvent.Raise();
        //}
            
       
    }
    void HandleHMDMounted()
    {
        Debug.Log("headset removed ");
    }

    void HandleHMDUnmounted()
    {
        Debug.Log("headset on head ");
    }

    void HandleHMDLost()
    {
        Debug.Log("headset deattached ");
    }
}
