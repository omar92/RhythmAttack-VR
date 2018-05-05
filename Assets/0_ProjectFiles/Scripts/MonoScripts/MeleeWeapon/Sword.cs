using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Sword : MonoBehaviour
{

    SteamVR_TrackedObject strackObject = null;
    SteamVR_Controller.Device sdevice;

    AudioSource audioSource;
    Dictionary<string, AudioClip> AudioLib = new Dictionary<string, AudioClip>();
    // Use this for initialization

    Vector3 currentPos;
    Vector3 previuosPos;

    public float speed;

    
    
    void Start()
    {
        previuosPos = Vector3.zero;

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;

        speed = (currentPos - previuosPos).magnitude / Time.deltaTime;

        previuosPos = currentPos;
       // Debug.Log(" controler speed = " + speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision");
        if (collision.gameObject.tag == "Minion")
        {
          //  Destroy(collision.gameObject);
            //sdevice.TriggerHapticPulse(700);
            //
       //   SteamVR_Controller.Input((int)LController.trackObject.index).TriggerHapticPulse(1000);
       //   SteamVR_Controller.Input((int )LController.trackObject.index).TriggerHapticPulse(1000);
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("collision");
        if (other.gameObject.tag == "Note")
        {
       //     Destroy(other.gameObject);
            //sdevice.TriggerHapticPulse(700);
            //
        //  SteamVR_Controller.Input(LController.currentDeviceIndex).TriggerHapticPulse(1000);
       //   SteamVR_Controller.Input(LController.currentDeviceIndex).TriggerHapticPulse(1000);

            NoteScript sc = other.GetComponent<NoteScript>();

            audioSource.Play();

            //var key = sc.MidiWave.WaveFile.Replace(".wav", "");
            //if (AudioLib.ContainsKey(key))
            //{
            //    audioSource.pitch = sc.MidiWave.Patch;
            //  //  audioSource.volume = sc.MidiWave.v;
            //         audioSource.PlayOneShot(AudioLib[key]);
            //}
               
       
            //  print(sc.BeatName);
            // audioSource.clip = 
        }
    }
}
