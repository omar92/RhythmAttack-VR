using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsNoteScript : ANote
{
    public GameEvent gameEvent;

    internal void Spawn(Vector3 source, Vector3 dist, GameEvent gameEvent, Emitter emitter)
    {
        Spawn(source, dist);
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
