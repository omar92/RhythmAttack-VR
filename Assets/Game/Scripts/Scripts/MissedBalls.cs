using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedBalls : MonoBehaviour
{

    //public GameObject healthCapsules; 
    public FloatVariable playerHealth;
    public LevelSettings levelSettings;
    public GameEvent noteMissedE;
    public GameEvent playerHealthChangeE;
    public GameEvent LoseE;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Note")
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
            noteMissedE.Raise();
        }

    }
}
