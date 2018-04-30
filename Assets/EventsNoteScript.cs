using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsNoteScript : MonoBehaviour
{
    public EmitterEvents emitterEvent;
    public Emitter Emitter;
    public EmittedPorjectilesSettings settings;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //noteScript = GetComponent<NoteScript>();
    }




    internal void Spawn(Vector3 position, EmitterEvents emitterEvent, Emitter emitter)
    {
        // this.enabled = true;
        transform.SetParent(null);
        transform.position = position;
        rb.GetComponent<Renderer>().enabled = true;
        rb.GetComponent<Collider>().enabled = true;
        rb.velocity = new Vector3(0, 0, -settings.Velocity);
       this.emitterEvent = emitterEvent;
    }

    private void OnTriggerEnter(Collider other)
    {

    }

}
