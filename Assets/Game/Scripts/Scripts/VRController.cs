using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour {


    //public
    public VRControllersTypes vrControllerType;
    public Transform AssociatedTransform;

    //Private
     SteamVR_TrackedObject trackObject = null;
     SteamVR_Controller.Device device;

 
    public static int currentDeviceIndex;
    Color currentColler;
    int ChildCount = 0;
    IControllable child;
    void Awake()
    {
        trackObject = GetComponent<SteamVR_TrackedObject>();
        trackObject.SetDeviceIndex((int)vrControllerType); 
    }
    void Start()
    {
        
    }
    
    void Update()
    {

        AssociatedTransform.position = transform.position;
        AssociatedTransform.rotation = transform.rotation;

        device = SteamVR_Controller.Input((int)trackObject.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            ExcuteInChildren((child) =>
            {
                child.OnTrigger(true);
            });
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            ExcuteInChildren((child) =>
            {
                child.OnTrigger(false);
            });
        }
    }

    void ExcuteInChildren(Action<IControllable> action)
    {
        ChildCount = AssociatedTransform.childCount;
        for (int i = 0; i < ChildCount; i++)
        {
            child = AssociatedTransform.GetChild(i).GetComponent<IControllable>();
            if (child != null)
            {
                action(child);
            }
        }
    }
}
