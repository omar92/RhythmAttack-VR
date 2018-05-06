using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{

    //public  Material[] healthMaterials = new Material[4];
    public static float playerHealth = 100;

    static Transform capsuleTransform;

    static Transform first;
    static Transform second;
    static Transform third;
    static Transform fourth;

    void Start()
    {
        capsuleTransform = this.transform;
        for (int i = 0; i < capsuleTransform.childCount; i++)
        {
            capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 255, 255, 255);

        }
        first = capsuleTransform.GetChild(0);
        second = capsuleTransform.GetChild(1);
        third = capsuleTransform.GetChild(2);
        fourth = capsuleTransform.GetChild(3);
    }


    public static void healthMatChange()
    {
        Debug.Log(capsuleTransform.childCount);

        if (playerHealth > 75)
        {
            for (int i = 0; i < capsuleTransform.childCount; i++)
            {
                capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 255, 255, 255);

            }


        }
        else if (playerHealth > 50 && playerHealth < 76)
        {

            for (int i = 0; i < capsuleTransform.childCount; i++)
            {
                capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = Color.blue;

            }
            if (first != null)
            {
                Destroy(first.gameObject);

            }

        }
        else if (playerHealth > 25 && playerHealth < 51)
        {

            for (int i = 0; i < capsuleTransform.childCount; i++)
            {
                capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = Color.green;

            }

            if (second != null)
            {

                Destroy(second.gameObject);

            }
        }
        else if (playerHealth > 0 && playerHealth < 26)
        {

            for (int i = 0; i < capsuleTransform.childCount; i++)
            {
                capsuleTransform.GetChild(i).GetComponent<Renderer>().material.color = Color.red;

            }

            if (third != null)
            {
                Destroy(third.gameObject);

            }


        }


    }
}
