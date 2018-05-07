using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Sword : MonoBehaviour
{
     public FloatVariable speed;
    // Use this for initialization
    Vector3 currentPos;
    Vector3 previuosPos;
    AudioSource audioSource;
    void Start()
    {
        previuosPos = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        currentPos = transform.position;
        speed.value = (currentPos - previuosPos).magnitude / Time.deltaTime;
        previuosPos = currentPos;
       // Debug.Log(" controler speed = " + speed);

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
            NoteScript sc = other.GetComponent<NoteScript>();
            audioSource.Play();
        }
    }
}
