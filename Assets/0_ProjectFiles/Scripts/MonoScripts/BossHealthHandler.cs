using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthHandler : MonoBehaviour {


    public GameEvent healthChangedE;
    public GameEvent WinE;
    public FloatVariable BossHealth;
    public LevelSettings settings;

    public void OnBossReciveDamage()
    {
        BossHealth.value -= settings.GunDamage;
        if (BossHealth.value < 0) { BossHealth.value = 0; }
        print("Boss health" + BossHealth.value);
        healthChangedE.Raise();
        if (BossHealth.value == 0)
        {
            WinE.Raise();
            print("Win");
        }

    }

}
