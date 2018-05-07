using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MidiHandler : MonoBehaviour
{

    public LevelSounds levelSounds;

    public UnityEvent OnInitDone;
    public void InitMidi()
    {
        MidiPlayerInitialiser.Init(levelSounds.DefenceMIDI);

        string[] tracks = new string[2];
        tracks[0] = levelSounds.DefenceMIDI;
        tracks[1] = levelSounds.AttackMIDI;
       // = MidiPlayerInitialiser.GetTracksLocations(levelSounds.DefenceMIDI);
        Dictionary<int, int> trackMapper = new Dictionary<int, int>();
        // Debug.Log("-----------------------------------------------");
        //  for (int i = 0; i < tracks.Length; i++)
        // {
            int i=1; //update this
            var notes = MidiAnalyiser.GetMidiNotesTypes(tracks[i]);
            Array.Sort(notes);
            for (int x = 0; x < notes.Length; x++)
            {
                Debug.Log(notes[x] + ": " + x);
                trackMapper.Add(notes[x], x);
            }
            GlobalData.tracksNotesLanesMaper.Add(trackMapper);
            Debug.Log("-----------------------------------------------");
      //  }
        OnInitDone.Invoke();

    }
}
