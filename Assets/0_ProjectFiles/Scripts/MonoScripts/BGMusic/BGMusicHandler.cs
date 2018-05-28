using System.Collections;
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
    public void AssignDefenceClip()
    {
        int index = GetBgIndex();
        audioSource.clip = levelSounds.DefenceLevels[index].BG;
    }
    public void AssignAttackClip()
    {
        int index = GetBgIndex();
        audioSource.clip = levelSounds.AttackLevels[index].BG;
    }

    private int GetBgIndex()
    {
        return GameLevel.value <= levelSounds.DefenceLevels.Count ? (int)GameLevel.value - 1 : levelSounds.DefenceLevels.Count - 1;
    }

}
