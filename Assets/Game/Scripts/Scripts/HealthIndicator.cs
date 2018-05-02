using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour {

    //public  Material[] healthMaterials = new Material[4];
    public  static float playerHealth=100;

    static Transform  capsuleTransform;


    void Start()
    {
        capsuleTransform = this.transform;
        for (int i = 0; i < capsuleTransform.childCount; i++)
        {
            capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 255, 255, 255);

        }
    }

    
    public static void healthMatChange()
    {
        Debug.Log(capsuleTransform.childCount);
         
        if (playerHealth > 75)
        {
            for (int i= 0; i< capsuleTransform.childCount;i++ )
            {
                capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color= new Color(0,255,255,255);
               
            }
      
        }
        else if (playerHealth > 50 && playerHealth < 76)
        {
            if (capsuleTransform.GetChild(capsuleTransform.childCount - 1) != null)
            {
                for (int i = 0; i < capsuleTransform.childCount; i++)
            {
                capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 0, 255);

            }
            
                Destroy(capsuleTransform.GetChild(capsuleTransform.childCount - 1).gameObject);
            }
           
        }
        else if (playerHealth > 25 && playerHealth < 51)
        {
            if (capsuleTransform.GetChild(capsuleTransform.childCount - 1) != null)
            {
                for (int i = 0; i < capsuleTransform.childCount; i++)
            {
                capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 255, 0);

            }
            
                Destroy(capsuleTransform.GetChild(capsuleTransform.childCount - 1).gameObject);
            }
        }
        else if (playerHealth > 0 && playerHealth < 26)
        {
            if (capsuleTransform.GetChild(capsuleTransform.childCount -1) != null)
            {
                for (int i = 0; i < capsuleTransform.childCount; i++)
                {
                    capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 0, 255);

                }


                Destroy(capsuleTransform.GetChild(capsuleTransform.childCount - 1).gameObject);
            }
               
            
        }


    }
}
