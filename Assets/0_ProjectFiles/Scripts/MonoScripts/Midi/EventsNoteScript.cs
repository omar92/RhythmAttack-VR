using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsNoteScript : MonoBehaviour
{
    public GameEvent gameEvent;
    public EmittedPorjectilesSettings settings;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    internal void Spawn(Vector3 position, GameEvent gameEvent, Emitter emitter)
    {
        transform.SetParent(null);
        transform.position = position;
        rb.velocity = new Vector3(0, 0, -settings.Velocity);
        this.gameEvent = gameEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameEvent != null)
        {
            gameEvent.Raise();
        }
    }

}
