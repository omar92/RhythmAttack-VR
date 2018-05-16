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
    public FloatVariable finalScore;
    public FloatVariable combo;

    [Space()]
    [Header("Data To Clean")]
    public StateMachine stateMachine;


    public void InitGameData()
    {
        //
        PlayerHealth.value = levelSettings.PlayerHealth;
        BossHealth.value = levelSettings.BossHealth;
        finalScore.value = 0f;
        combo.value = 0f;

        //clean
        stateMachine.Init();
    }
}
