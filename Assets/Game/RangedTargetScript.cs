using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTargetScript : MonoBehaviour {

    public bool isDone = false;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void SpawnTargets() {
        isDone = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

    }


    public void Update()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
           if(transform.GetChild(i).gameObject.activeInHierarchy)
            {
                return;
            }
        }
      //Debug.Log("all targets destroyed");
        isDone = true;
    }


}
