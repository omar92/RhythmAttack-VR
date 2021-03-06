﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicHandler : MonoBehaviour
{
    public LevelSounds levelSounds;
    public FloatVariable GameLevel;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    //public void AssignDefenceClip()
    //{
    //    int index = GetBgIndex(levelSounds.DefenceLevels);
    //    audioSource.clip = levelSounds.DefenceLevels[index].BG;
    //}
    public void AssignDefenceClip()
    {
        audioSource.clip = levelSounds.DefenceLevelsBG((int)GameLevel.value - 1);
    }
    //public void AssignAttackClip()
    //{
    //    int index = GetBgIndex(levelSounds.AttackLevels);
    //    audioSource.clip = levelSounds.AttackLevels[index].BG;
    //}
    public void AssignAttackClip()
    {
        audioSource.clip = levelSounds.AttackLevelsBG((int)GameLevel.value - 1);
    }

    //private int GetBgIndex(List<LevelBgMidiMapper> list)
    //{
    //    return GameLevel.value <= list.Count ? (int)GameLevel.value - 1 : list.Count - 1;
    //}
    //private int GetBgIndex(int LevelsNum)
    //{
    //    return GameLevel.value <= LevelsNum ? (int)GameLevel.value - 1 : LevelsNum - 1;
    //}
}
