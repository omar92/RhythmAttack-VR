using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "LevelSounds", menuName = "GameSettings/LevelSounds", order = 1)]
public class LevelSounds : ScriptableObject
{
    [HideInInspector]
    public int defenceLevelsNum = 3;

    //[Header("Defence Level 1")]
    public AudioClip Level1_D_BG;
    public string Level1_D_MIDI;

    //[Header("Defence Level 2")]
    public AudioClip Level2_D_BG;
    public string Level2_D_MIDI;

    //[Header("Defence Level 3")]
    public AudioClip Level3_D_BG;
    public string Level3_D_MIDI;

    //  public List<LevelBgMidiMapper> DefenceLevels = new List<LevelBgMidiMapper>();
    [HideInInspector]
    public int attackLevelsNum = 1;

    //[Header("Attack Level 1")]
    public AudioClip Level1_A_BG;
    public string Level1_A_MIDI;


    //[Header("Attack Mode")]
    // public List<LevelBgMidiMapper> AttackLevels = new List<LevelBgMidiMapper>();

   // [Header("Lanes sounds")]
    public AudioClip[] LaneSounds = new AudioClip[8];

    internal AudioClip DefenceLevelsBG(int index)
    {
        switch (index)
        {
            case 0:
               return Level1_D_BG;
            case 1:
                return Level2_D_BG;
            case 2:
                return Level3_D_BG;
            default:
                return Level1_D_BG;
        }
    }
    internal string DefenceLevelsMIDI(int index)
    {
        switch (index)
        {
            case 0:
                return Level1_D_MIDI;
            case 1:
                return Level2_D_MIDI;
            case 2:
                return Level3_D_MIDI;
            default:
                return Level1_D_MIDI;
        }
    }
    internal AudioClip AttackLevelsBG(int v)
    {
        return Level1_A_BG; ;
    }
    internal string AttackLevelsMIDI(int v)
    {
        return Level1_A_MIDI; ;
    }
    //private void Awake()
    //{
    //    DefenceLevels[0] = new LevelBgMidiMapper();
    //    var x = DefenceLevels[0];
    //    x.MIDI = "";
    //    DefenceLevels[0] = x;
    //}
}
[System.Serializable]
public struct LevelBgMidiMapper
{
    public AudioClip BG;

    public string MIDI;


    public LevelBgMidiMapper(AudioClip BG, string MIDI)
    {
        this.BG = BG;
        this.MIDI = MIDI;
    }

    public LevelBgMidiMapper(LevelBgMidiMapper levelBgMidiMapper)
    {
        this.BG = levelBgMidiMapper.BG;
        this.MIDI = levelBgMidiMapper.MIDI;
    }
}
