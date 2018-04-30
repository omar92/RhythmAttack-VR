using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour {

    public Material[] healthMaterials = new Material[4];
    public float playerHealth=100;
    
    
    public void healthMatChange(float currentHealth)
    {
        if (currentHealth > 75)
        {
            for (int i= 0; i<transform.childCount;i++ )
            {
                transform.GetChild(i).GetComponent<Renderer>().material= healthMaterials[0];
               
            }
      
        }
        else if (currentHealth > 50 && currentHealth < 76)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = healthMaterials[1];

            }
            Destroy(transform.GetChild(transform.childCount-1));
        }
        else if (currentHealth > 25 && currentHealth < 51)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = healthMaterials[2];

            }
            Destroy(transform.GetChild(transform.childCount - 1));
        }
        else if (currentHealth > 0 && currentHealth < 26)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material = healthMaterials[3];

            }
            Destroy(transform.GetChild(transform.childCount - 1));
        }


    }
}
