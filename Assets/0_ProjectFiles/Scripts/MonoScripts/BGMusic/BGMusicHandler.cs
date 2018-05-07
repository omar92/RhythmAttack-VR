using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicHandler : MonoBehaviour
{
    public LevelSounds levelSounds;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void AssignDefenceClip()
    {
        audioSource.clip = levelSounds.DefenceBG;
    }

    public void AssignAttackClip()
    {
        audioSource.clip = levelSounds.AttackBG;
    }

}
