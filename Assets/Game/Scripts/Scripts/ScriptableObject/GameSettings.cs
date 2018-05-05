using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "GeneralSettings", menuName = "GameSettings/General", order = 1)]
class GameSettings : ScriptableObject
{

    [Header("Weapons sounds")]
    [Header("Melee sounds")]
    public AudioClip MeleeSlash;
    public AudioClip MeleeMiss;
    [Header("Ranged sounds")]
    public AudioClip RangedShoot;
    public AudioClip RangedHit;
    [Header("Other sounds")]
    public AudioClip NoteMiss;
    public AudioClip GameWin;
    public AudioClip GameLose;

}

