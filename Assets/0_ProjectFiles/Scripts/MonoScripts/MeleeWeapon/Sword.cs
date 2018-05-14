using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRTK;

public class Sword : MonoBehaviour
{
    public FloatVariable speed;
    
    public GameEvent ballCut;
    public GameEvent swordCut;
    // Use this for initialization
    public Vector3Variable currentPos;
    Vector3 previuosPos;
    AudioSource audioSource;
    //public VRTK_ControllerReference VRTK_Controller; 

    
    void Start()
    {
        previuosPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        //VRTK_Controller = GetComponent<VRTK_ControllerReference>();
    }

    void Update()
    {
<<<<<<< HEAD
        currentPos.value = transform.position;
        speed.value = (currentPos.value - previuosPos).magnitude / Time.deltaTime;
        previuosPos = currentPos.value;
       // Debug.Log(" controler speed = " + speed);
=======
        currentPos = transform.position;
        speed.value = (currentPos - previuosPos).magnitude / Time.deltaTime;
        previuosPos = currentPos;
        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(gameObject), speed.value, speed.value/3f, speed.value/10f);
        // Debug.Log(" controler speed = " + speed);
>>>>>>> a5121129540b783356d253d719e5e1f63e7e8b6f

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            ballCut.Raise();
            swordCut.Raise();
            //VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_Controller, 1f, 1f, 1f);
            NoteScript sc = other.GetComponent<NoteScript>();
            audioSource.Play();
        }
    }
}
