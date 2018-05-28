using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelSounds", menuName = "GameSettings/LevelSounds", order = 1)]
public class LevelSounds : ScriptableObject
{
    [Header("Defence Mode")]
    public List<LevelBgMidiMapper> DefenceLevels = new List<LevelBgMidiMapper>();

    [Header("Attack Mode")]
    public List<LevelBgMidiMapper> AttackLevels = new List<LevelBgMidiMapper>();

    [Header("Lanes sounds")]
    public AudioClip[] LaneSounds = new AudioClip[8];

    private void Awake()
    {
        DefenceLevels[0] = new LevelBgMidiMapper();
        var x = DefenceLevels[0];
        x.MIDI = "";
        DefenceLevels[0] = x;
    }
}
[System.Serializable]
public struct LevelBgMidiMapper
{
    public AudioClip BG;

    private string mIDI;

    public string MIDI
    {
        get
        {
            return mIDI;
        }

        set
        {
            mIDI = value;
        }
    }

    public LevelBgMidiMapper(AudioClip BG, string MIDI)
    {
        this.BG = BG;
        mIDI = MIDI;
    }

    public LevelBgMidiMapper(LevelBgMidiMapper levelBgMidiMapper)
    {
        this.BG = levelBgMidiMapper.BG;
        mIDI = levelBgMidiMapper.MIDI;
    }
}
