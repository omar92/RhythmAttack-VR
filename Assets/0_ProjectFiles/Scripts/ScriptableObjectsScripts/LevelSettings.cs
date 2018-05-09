using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "GameSettings/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    [Header("Level Settings")]
    public float PlayerHealth;
    public float BossHealth;
    public float NoteVelocity;
    public float GunDamage;
    public float minCutSpeed;
}
