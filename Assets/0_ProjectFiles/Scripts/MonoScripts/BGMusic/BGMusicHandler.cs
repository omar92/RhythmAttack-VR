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
        audioSource.clip = levelSounds.DefenceLevels[(int)GameLevel.value - 1].BG;
    }

    public void AssignAttackClip()
    {
        audioSource.clip = levelSounds.AttackLevels[(int)GameLevel.value - 1].BG;
    }

}
