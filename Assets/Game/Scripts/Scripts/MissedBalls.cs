using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedBalls : MonoBehaviour {

    //public GameObject healthCapsules; 
    public FloatVariable playerHealth;
    public GameEvent noteMissedE;
    public GameEvent LoseE;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Note")
        {
            if (playerHealth.value > 0)
            {
                    playerHealth.value -= 4;
            }
            else

            {
                LoseE.Raise();
            }
            noteMissedE.Raise();
        }
        
    }
}
