using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller : MonoBehaviour {

    public SteamVR_TrackedObject trackObject=null;
    public SteamVR_Controller.Device device;

    void Awake()
    {
        trackObject = GetComponent<SteamVR_TrackedObject>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackObject.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("down");
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("up");
        }
    }
}
