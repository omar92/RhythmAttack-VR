using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTargetScript : MonoBehaviour {

    public GameEvent attachEndE;
    public GameEvent healthChanged;
    GameObject Targets;

    public FloatVariable health;

    private void Awake()
    {
        Targets = GameObject.FindGameObjectWithTag("Targets");
    }

    private void Start()
    {      
        
    }

    public void SpawnTargets()
    {
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
        attachEndE.Raise();
    }

    public void TakeDamage(float amount)
    {
        health.value -= amount;

        if(health.value <= 0)
        {
            health.value  = 0;
        }
        healthChanged.Raise();
    }
    public void HideTargets()
    {
        for (int i = 0; i < Targets.transform.childCount; i++)
        {
            Targets.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
