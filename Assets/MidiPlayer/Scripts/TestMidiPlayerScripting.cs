using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using System;

public class TestMidiPlayerScripting : MonoBehaviour
{

    public MidiFilePlayer midiFilePlayer;
    public float LastTimeChange;
    public float DelayTimeChange = 5;
    public bool CheckPosition = false;
    public bool CheckSpeed = false;
    private void Awake()
    {
        MidiPlayerGlobal.OnEventPresetLoaded.AddListener(() => EndLoadingSF());
    }

    private void EndLoadingSF()
    {
        Debug.Log("End loading SF, MPTK is ready to play");
        Debug.Log("   Time To Load SoundFont: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadSoundFont.TotalSeconds, 3).ToString() + " second");
        Debug.Log("   Time To Load Waves: " + Math.Round(MidiPlayerGlobal.MPTK_TimeToLoadWave.TotalSeconds, 3).ToString() + " second");
        Debug.Log("   Presets Loaded: " + MidiPlayerGlobal.MPTK_CountPresetLoaded);
        Debug.Log("   Waves Loaded: " + MidiPlayerGlobal.MPTK_CountWaveLoaded);
    }
    // Use this for initialization
    void Start()
    {
        InitPlay();
    }
    public void InitPlay()
    {
        // Find first midi which contains this string
        int index = MidiPlayerGlobal.MPTK_FindMidi("0007");
        if (index >= 0)
        {
            midiFilePlayer.MPTK_MidiIndex = index;
            midiFilePlayer.MPTK_Play();
            LastTimeChange = Time.realtimeSinceStartup;
        }
    }

    /// <summary>
    /// Triggered at end of each midi file 
    /// </summary>
    public void RandomPlay()
    {
        Debug.Log("Is playing : " + midiFilePlayer.MPTK_IsPlaying);
        int index = UnityEngine.Random.Range(0, MidiPlayerGlobal.MPTK_ListMidis.Count);
        midiFilePlayer.MPTK_MidiIndex = index;
        midiFilePlayer.MPTK_Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (midiFilePlayer != null && midiFilePlayer.MPTK_IsPlaying)
        {
            float time = Time.realtimeSinceStartup - LastTimeChange;
            if (time > DelayTimeChange)
            {
                LastTimeChange = Time.realtimeSinceStartup;
                if (CheckPosition)
                {
                    midiFilePlayer.MPTK_Position = UnityEngine.Random.Range(0f, (float)midiFilePlayer.MPTK_Duration.TotalMilliseconds);
                }
                if (CheckSpeed)
                {
                    midiFilePlayer.MPTK_Speed = UnityEngine.Random.Range(0.1f, 5f);
                }
            }
        }
    }
}
