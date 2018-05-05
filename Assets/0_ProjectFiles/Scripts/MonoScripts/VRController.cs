using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour
{


    //public
    public VRControllersTypes vrControllerType;
    public TransformVariable AssociatedTransform;

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
    void FixedUpdate()
    {
        AssociatedTransform.value.position = transform.position;
        AssociatedTransform.value.rotation = transform.rotation;
    }
    void Update()
    {



        device = SteamVR_Controller.Input((int)trackObject.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            ExcuteInChildren((child) =>
            {
                child.OnTrigger(true);
            });
        }
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            ExcuteInChildren((child) =>
            {
                child.OnTrigger(false);
            });
        }
    }

    void ExcuteInChildren(Action<IControllable> action)
    {
        ChildCount = AssociatedTransform.value.childCount;
        for (int i = 0; i < ChildCount; i++)
        {
            child = AssociatedTransform.value.GetChild(i).GetComponent<IControllable>();
            if (child != null)
            {
                action(child);
            }
        }
    }
}
