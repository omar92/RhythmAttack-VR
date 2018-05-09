using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MidiHandler : MonoBehaviour
{

    public LevelSounds levelSounds;
    public string tracksResurcesFolder;
    public UnityEvent OnInitDone;
    public void InitMidi()
    {
        var tracksPaths = new string[]
        {
            tracksResurcesFolder+levelSounds.DefenceMIDI,
            tracksResurcesFolder+levelSounds.AttackMIDI
        };
        MidiPlayerInitialiser.Init(tracksPaths);

        string[] tracks = new string[2];
        tracks[0] = levelSounds.DefenceMIDI;
        tracks[1] = levelSounds.AttackMIDI;

        Dictionary<int, int> trackMapper;
        for (int i = 0; i < tracks.Length; i++)
        {
            trackMapper = new Dictionary<int, int>();
            var notes = MidiAnalyiser.GetMidiNotesTypes(tracksResurcesFolder + tracks[i]);
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
