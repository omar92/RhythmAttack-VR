using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LController : MonoBehaviour {

    public bool isLeft;
    public static SteamVR_TrackedObject trackObject = null;
    public SteamVR_Controller.Device device;

  //  public GameObject mysword;
    public static int currentDeviceIndex;
    Color currentColler;
    int ChildCount = 0;
    IControllable child;
    void Awake()
    {
        trackObject = GetComponent<SteamVR_TrackedObject>();
    }
    void Start()
    {
       // currentColler = mysword.GetComponent<Renderer>().material.color;
        //currentDeviceIndex = (int)this.device.index;
        if(isLeft)
            trackObject.SetDeviceIndex(8);
        else
            trackObject.SetDeviceIndex(9);
    }

    void Update()
    {

        device = SteamVR_Controller.Input((int)trackObject.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            HandCheck(true);
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            HandCheck(false);
        }
    }
    void HandCheck(bool hand)
    {
        ChildCount = transform.childCount;
        for (int i = 0; i < ChildCount; i++)
        {
            child = transform.GetChild(i).GetComponent<IControllable>();
            if (child != null)
            {
                child.OnTrigger(hand);
            }
        }
    }
}
