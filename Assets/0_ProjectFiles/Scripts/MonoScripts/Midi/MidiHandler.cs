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
    public FloatVariable GameLevel;


    List<string> tracksPaths;
    Coroutine co;
    private MidiPlayerTK.MidiFilePlayer player;
    public void InitMidi()
    {
        if (co == null)
            co = StartCoroutine(InitMidiData());

    }

    private IEnumerator InitMidiData()
    {
        tracksPaths = new List<string>();
        player = GetComponent<MidiPlayerTK.MidiFilePlayer>();

        for (int i = 0; i < levelSounds.DefenceLevels.Count; i++)
        {
            tracksPaths.Add(levelSounds.DefenceLevels[i].MIDI);
        }
        for (int i = 0; i < levelSounds.AttackLevels.Count; i++)
        {
            tracksPaths.Add(levelSounds.AttackLevels[i].MIDI);
        }
        MidiPlayerInitialiser.Init(tracksPaths.ToArray(), tracksResurcesFolder);


        Dictionary<int, int> trackMapper;

        //defence tracks lanes mapper
        for (int i = 0; i < levelSounds.DefenceLevels.Count; i++)
        {
            trackMapper = new Dictionary<int, int>();
            var notes = MidiAnalyiser.GetMidiNotesTypes(tracksResurcesFolder + levelSounds.DefenceLevels[i].MIDI);
            Array.Sort(notes);
            for (int x = 0; x < notes.Length; x++)
            {
                Debug.Log(notes[x] + ": " + (notes[x] - notes[0]));
                trackMapper.Add(notes[x], notes[x] - notes[0]);
            }
            GlobalData.DefenceTracksNotesLanesMaper.Add(trackMapper);
            //Debug.Log("-----------------------------------------------");
        }

        //Attack tracks lanes mapper
        for (int i = 0; i < levelSounds.AttackLevels.Count; i++)
        {
            trackMapper = new Dictionary<int, int>();
            var notes = MidiAnalyiser.GetMidiNotesTypes(tracksResurcesFolder + levelSounds.AttackLevels[i].MIDI);
            Array.Sort(notes);
            for (int x = 0; x < notes.Length; x++)
            {
                Debug.Log(notes[x] + ": " + (notes[x] - notes[0]));
                trackMapper.Add(notes[x], notes[x] - notes[0]);
            }
            GlobalData.AttackTracksNotesLanesMaper.Add(trackMapper);
            //Debug.Log("-----------------------------------------------");
        }

        yield return new WaitForEndOfFrame();
        co = null;
        OnInitDone.Invoke();
    }


    public void SetLevelDefenceMidi()
    {
        
        SetLevelDefenceMidi(GetMIDIIndex(levelSounds.DefenceLevels));
    }
    public void SetLevelDefenceMidi(int index)
    {

      //  Debug.Log("index " + index);
        player.MPTK_MidiIndex = index;
        player.MPTK_Position = 0;
    }


    public void SetLevelAttackMidi()
    {
        SetLevelAttackMidi(GetMIDIIndex(levelSounds.AttackLevels));
    }
    public void SetLevelAttackMidi(int index)
    {
      //  Debug.Log("levelSounds.DefenceLevels.Count + index " + (levelSounds.DefenceLevels.Count + index));
        player.MPTK_MidiIndex = levelSounds.DefenceLevels.Count + index;
        player.MPTK_Position = 0;
    }



    //private int GetMIDIIndex()
    //{
    //    return GameLevel.value <= levelSounds.DefenceLevels.Count ? (int)GameLevel.value - 1 : levelSounds.DefenceLevels.Count - 1;
    //}

    private int GetMIDIIndex(List<LevelBgMidiMapper> list)
    {
        return GameLevel.value < list.Count ? (int)GameLevel.value - 1 : list.Count -1 ;
    }

    public int GetMIDIIndex(int listCount)
    {
        return GameLevel.value < listCount ? (int)GameLevel.value - 1 : listCount - 1;
    }
}
