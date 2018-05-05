using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MidiHandler : MonoBehaviour
{

    public string TracksPath = "Assets/Game/Resources/Tracks/";

    public UnityEvent OnInitDone;
    public void InitMidi()
    {
        MidiPlayerInitialiser.Init(TracksPath);

        var tracks = MidiPlayerInitialiser.GetTracksLocations(TracksPath);
        Dictionary<int, int> trackMapper = new Dictionary<int, int>();
        Debug.Log("-----------------------------------------------");
        for (int i = 0; i < tracks.Length; i++)
        {
            var notes = MidiAnalyiser.GetMidiNotesTypes(tracks[i]);
            Array.Sort(notes);
            for (int x = 0; x < notes.Length; x++)
            {
                Debug.Log(notes[x] + ": " + x);
                trackMapper.Add(notes[x], x);
            }
            GlobalData.tracksNotesLanesMaper.Add(trackMapper);
            Debug.Log("-----------------------------------------------");
        }
        OnInitDone.Invoke();

    }
}
