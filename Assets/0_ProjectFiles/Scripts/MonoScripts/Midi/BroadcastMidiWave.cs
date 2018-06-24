using System;
using System.Collections;
using System.Collections.Generic;
using MidiPlayerTK;
using UnityEngine;


public class BroadcastMidiWave : MonoBehaviour
{

    public GameEvent NoteEvent;
    public ObjectVariable NoteData;

    public static BroadcastMidiWave inistance;

    // Use this for initialization
    void Awake()
    {
        inistance = this;

    }
    private void Start()
    {
        
    }

    internal void Broadcast(AudioSource audioSource, MidiNote note)
    {
        //Debug.Log("MIDI note Broadcast");
        NoteData.value = new MidiNoteAudio
        {
            audioSource = audioSource,
            note = note
        };

        NoteEvent.Raise();
        //BroadcastMessage("OnMidiNoteAudio", new MidiNoteAudio
        //{
        //    audioSource = audioSource,
        //    note = note
        //});
    }
}

public struct MidiNoteAudio
{
 //   public AudioSource audioSource;
    public MidiNote note;
}
