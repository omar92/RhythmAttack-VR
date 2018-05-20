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
    Coroutine co;

    public void InitMidi()
    {
        if (co == null)
            co = StartCoroutine(InitMidiData());

    }

    private IEnumerator InitMidiData()
    {
      //  yield return new WaitForEndOfFrame();
        tracksPaths = new string[]
        {
          tracksResurcesFolder+levelSounds.DefenceMIDI,
          tracksResurcesFolder+levelSounds.AttackMIDI
        };
        MidiPlayerInitialiser.Init(tracksPaths);

      //  yield return new WaitForEndOfFrame();

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
            co = null;
        }

        yield return new WaitForEndOfFrame();
        OnInitDone.Invoke();
    }
}
