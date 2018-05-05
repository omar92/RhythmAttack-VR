using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "GameSettings/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{

    public LevelSounds levelSounds;
    
    public int BossHealth;
    public int MenionsSpeed;
    public int GunDamage;




}
