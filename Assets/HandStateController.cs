using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStateController : MonoBehaviour {

    public Sword Melee;
    public Gun Ranged;

    // Use this for initialization
    void Start () {
        Melee.gameObject.SetActive(false);
        Ranged.gameObject.SetActive(true);
    }
	
    public void SetHandState(HandStates state)
    {
        Melee.gameObject.SetActive(state == HandStates.Melee);
        Ranged.gameObject.SetActive(state == HandStates.Ranged);
    }
}
