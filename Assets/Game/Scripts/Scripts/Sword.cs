using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Sword : MonoBehaviour {

     SteamVR_TrackedObject strackObject = null;
     SteamVR_Controller.Device sdevice;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag=="Minion")
        {
            Destroy(collision.gameObject);
            sdevice.TriggerHapticPulse(700);
        }
    }
}
