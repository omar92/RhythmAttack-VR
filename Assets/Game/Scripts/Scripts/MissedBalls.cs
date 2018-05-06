using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedBalls : MonoBehaviour {

    public GameObject healthCapsules; 
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Note")
        {
            if (HealthIndicator.playerHealth>0)
            {
                HealthIndicator.playerHealth -= 4;
                HealthIndicator.healthMatChange();
               // Debug.Log(HealthIndicator.playerHealth);
            }
            else
            {
                Destroy(healthCapsules);
            }
            
        }
        
    }
}
