using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataInitialiser : MonoBehaviour
{
    [Header("Level Data")]
    public LevelSettings levelSettings;
    [Space()]
    [Header("Game Data")]
    public FloatVariable PlayerHealth;
    public FloatVariable BossHealth;
    [Space()]
    [Header("Data To Clean")]
    public StateMachine stateMachine;


    public void InitGameData()
    {
        //
        PlayerHealth.value = levelSettings.PLayerHealth;
        BossHealth.value = levelSettings.BossHealth;

        //clean
        stateMachine.Init();
    }
}
