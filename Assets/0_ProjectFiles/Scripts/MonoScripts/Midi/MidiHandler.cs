using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MidiHandler : MonoBehaviour
{

    public LevelSounds levelSounds;
    public string tracksResurcesFolder;
    public UnityEvent OnInitDone;
    string[] tracksPaths;
    public void Awake()
    {
        tracksPaths = new string[]
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
                Debug.Log(notes[x] + ": " + (notes[x] - notes[0]));
                trackMapper.Add(notes[x], notes[x] - notes[0]);
            }
            GlobalData.tracksNotesLanesMaper.Add(trackMapper);
            //Debug.Log("-----------------------------------------------");
        }
    }

    public void InitMidi()
    {

        OnInitDone.Invoke();
    }

}
