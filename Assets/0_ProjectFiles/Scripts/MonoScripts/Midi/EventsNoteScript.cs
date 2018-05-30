using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsNoteScript : MonoBehaviour
{
    public GameEvent gameEvent;
    public LevelSettings settings;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    internal void Spawn(Vector3 source, Vector3 dist, GameEvent gameEvent, Emitter emitter)
    {
        transform.SetParent(null);
        transform.position = source;

        var newDistance = Vector3.Distance(source, dist);
        float newVelocity = newDistance / settings.NoteVelocity;
        var dir = (dist - source).normalized * newVelocity;
        rb.velocity = dir;

        this.gameEvent = gameEvent;
    }

    public void Hide()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameEvent != null)
        {
            gameEvent.Raise();
        }
    }

}
