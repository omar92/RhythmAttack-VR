using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegeneration : MonoBehaviour {

    public FloatVariable playerHealth;
    public LevelSettings levelSettings;
    public GameEvent playerHealthChangeE;
    public void HealPlayer()
    {
        playerHealth.value += levelSettings.PlayerHealValue;
        playerHealthChangeE.Raise();
    }
}
