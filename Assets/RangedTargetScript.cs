using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTargetScript : MonoBehaviour {

    public bool isDone = false;
    GameObject Targets;

    public float health = 50f;
    private float originalHealth;
    HealthBarControll bar;

    private void Start()
    {
        originalHealth = health;
        Targets = GameObject.FindGameObjectWithTag("Targets");
        for (int i = 0; i < Targets.transform.childCount; i++) 
        {
            Targets.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SpawnTargets()
    {
        isDone = false;
        for (int i = 0; i < Targets.transform.childCount; i++)
        {
            Targets.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        for (int i = 0; i < Targets.transform.childCount; i++)
        {
           if(Targets.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return;
            }
        }
        isDone = true;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        bar.ChangeBar(health/ originalHealth);

        if(health < 0)
        {
            isDone = true;
        }
    }



}
