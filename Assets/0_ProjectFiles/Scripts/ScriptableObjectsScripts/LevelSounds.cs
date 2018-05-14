using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelSounds", menuName = "GameSettings/LevelSounds", order = 1)]
public class LevelSounds : ScriptableObject
{
    [Header("Defence Mode")]
    public AudioClip DefenceBG;
    public string DefenceMIDI= "Not Selected";

    [Header("Attack Mode")]
    public AudioClip AttackBG;
    public string AttackMIDI = "Not Selected";

    [Header("Lanes sounds")]
    public AudioClip[] LaneSounds = new AudioClip[8];



}
