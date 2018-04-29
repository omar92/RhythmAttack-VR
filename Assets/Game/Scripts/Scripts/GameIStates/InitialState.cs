using MidiPlayerTK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IState
{
    MidiFilePlayer midiPLayer;
    public void Enter(StateMachine stateMachine, GameManager gameManager)
    {
        midiPLayer = gameManager.GetComponentInChildren<MidiFilePlayer>();
        if (midiPLayer)
        {
            midiPLayer.enabled = false;
        }
        MidiPlayerInitialiser.Init("Assets/Game/Resources/Tracks/");

        var tracks = MidiPlayerInitialiser.GetTracksLocations("Assets/Game/Resources/Tracks/");
        Dictionary<int, int> trackMapper = new Dictionary<int, int>();
        Debug.Log("-----------------------------------------------");
        for (int i = 0; i < tracks.Length; i++)
        {
           var notes = MidiAnalyiser.GetMidiNotesTypes(tracks[i]);
            Array.Sort(notes);
            for (int x = 0; x < notes.Length; x++)
            {
                Debug.Log(notes[x] + ": "+ x);
                trackMapper.Add(notes[x], x);
            }
            GlobalData.tracksNotesLanesMaper.Add(trackMapper);
            Debug.Log("-----------------------------------------------");
        }
      
        stateMachine.ChangeState(new IntroState());
    }

    public void Excute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        if (midiPLayer)
        {
            midiPLayer.enabled = true;
        }
    }

}
