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
    public bool NoHealthLose = false;

    public void OnNoteMiss()
    {
        if (!NoHealthLose)
        {
            if (playerHealth.value > 0)

                playerHealth.value -= levelSettings.MissNoteDamage;
            if (playerHealth.value < 0)
            {
                playerHealth.value = 0;
            }
            print("playerHealth " + playerHealth.value);
            playerHealthChangeE.Raise();
            if (playerHealth.value == 0)
            {
                LoseE.Raise();
                print("Lose");
            }
        }
    }

    public void SetNoHealthLose(bool val)
    {
        NoHealthLose = val;
    }
}
