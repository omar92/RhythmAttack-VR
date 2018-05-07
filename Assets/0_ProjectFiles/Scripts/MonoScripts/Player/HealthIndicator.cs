using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{

    //public  Material[] healthMaterials = new Material[4];
    public  FloatVariable playerHealth ;

    public Transform first;
    public Transform second;
    public Transform third;
    public Transform fourth;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 255, 255, 255);

        }

    }


    public void HealthMatChange()
    {
        //Debug.Log(transform.childCount);

        if (playerHealth.value > 75)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = new Color(0, 255, 255, 255);

            }


        }
        else if (playerHealth.value > 50 && playerHealth.value < 76)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = Color.blue;

            }
            if (first != null)
            {
                Destroy(first.gameObject);

            }

        }
        else if (playerHealth.value > 25 && playerHealth.value < 51)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = Color.green;

            }

            if (second != null)
            {

                Destroy(second.gameObject);

            }
        }
        else if (playerHealth.value > 0 && playerHealth.value < 26)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = Color.red;

            }

            if (third != null)
            {
                Destroy(third.gameObject);

            }


        }


    }
}
