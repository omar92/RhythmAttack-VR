using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Sword : MonoBehaviour {

     SteamVR_TrackedObject strackObject = null;
     SteamVR_Controller.Device sdevice;

    AudioSource audioSource ;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();

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
            //sdevice.TriggerHapticPulse(700);
            //
            SteamVR_Controller.Input(RController.currentDeviceIndex).TriggerHapticPulse(1000);
            SteamVR_Controller.Input(LController.currentDeviceIndex).TriggerHapticPulse(1000);
            audioSource.Play();
        }
    }
}
