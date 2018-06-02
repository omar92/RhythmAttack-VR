using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{

    //public GameObject healthCapsules; 
    public FloatVariable playerHealth;
    public LevelSettings levelSettings;
    public GameEvent playerHealthChangeE;
    public GameEvent LoseE;

    public void OnNoteMiss()
    {
        if (playerHealth.value > 0)
        {
            playerHealth.value -= levelSettings.MissNoteDamage;
            if (playerHealth.value < 0)
            {
                playerHealth.value = 0;
            }
        }
        else

        {
            LoseE.Raise();
        }
        playerHealthChangeE.Raise();
    }

}
