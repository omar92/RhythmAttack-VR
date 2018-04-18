using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller : MonoBehaviour {

    SteamVR_TrackedObject trackObject=null;
    public SteamVR_Controller.Device device;
    public GameObject mysword;
    Color current;
    void Awake()
    {
        trackObject = GetComponent<SteamVR_TrackedObject>();
    }
    // Use this for initialization
    void Start () {
        current = mysword.GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackObject.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            mysword.GetComponent<Renderer>().material.color = new Color(0,1,1,1);
            Debug.Log("down");
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            mysword.GetComponent<Renderer>().material.color = current;
           Debug.Log("up");
        }
    }
}
