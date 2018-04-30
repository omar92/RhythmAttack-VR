using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class Emitter : MonoBehaviour
{

    public Transform EventEmitter;
    public EventsNoteScript eventNotePref;
    public static Emitter inistance = null;
    MidiFilePlayer midiPLayer;
    public AudioSource BgPlayer;

    public bool IsDone = false;

    internal void OnReciveEvent(EmitterEvents emitterEvent)
    {
        print(emitterEvent);
        switch (emitterEvent)
        {
            case EmitterEvents.StartBgMusic:
                BgPlayer.time = 0f;
                BgPlayer.Play();
                break;
            case EmitterEvents.PuseBgMusic:
                BgPlayer.Pause();
                break;
            case EmitterEvents.ResumeBgMusic:
                BgPlayer.UnPause();
                break;
            case EmitterEvents.StopBgMusic:
                IsDone = true;
                BgPlayer.Stop();
                break;
            default:
                break;
        }
    }

    public void Awake()
    {
        inistance = this;
        midiPLayer = GameObject.FindObjectOfType<MidiFilePlayer>();
      //  BgPlayer = GetComponentInChildren<AudioSource>();

        var eventCollectorPos = EventEmitter.position;
        var player = GameObject.FindGameObjectWithTag("Player");
        eventCollectorPos.z = player.transform.position.z;
        var emitter = GameObject.Instantiate(EventEmitter, eventCollectorPos, Quaternion.identity);
        emitter.gameObject.AddComponent<EmitterEventsCollector>();

    }

    public void StartEmitiing()
    {
        if (midiPLayer)
        {
            midiPLayer.MPTK_Play();
        }
        EmitEvent(EmitterEvents.StartBgMusic);
        IsDone = false;
    }

    void OnMidiNoteAudio(object data)
    {
        var noteAudio = (MidiNoteAudio)data;
        SpawnNote(noteAudio);
    }


    void SpawnNote(MidiNoteAudio note)
    {
        var em = GetLane(GlobalData.tracksNotesLanesMaper[0][note.note.Midi]);
        NoteScript clone = NotesPoolScript.inistance.PullNote(em.position, note);
    }


    Transform GetLane(int row, int col)
    {
        return transform.GetChild(row).GetChild(col);
    }
    Transform GetLane(int index)
    {
        return GetLane((index / (transform.GetChild(0).childCount)), index);
    }

   public void EmitEvent(EmitterEvents emitterEvent)
    {
        var eventNote = Instantiate(eventNotePref).GetComponent<EventsNoteScript>();
        // note.ObjectBool = transform;
        eventNote.tag = "Note";
        eventNote.Spawn(EventEmitter.position, emitterEvent, this);
    }

    public void OnMidiEnd()
    {
        EmitEvent(EmitterEvents.StopBgMusic);
    }
}

