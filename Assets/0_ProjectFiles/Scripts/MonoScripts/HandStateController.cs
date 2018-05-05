using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStateController : MonoBehaviour {

    public Sword Melee;
    public Gun Ranged;

    // Use this for initialization
    void Awake() {
       // SetHandState(HandStates.Empty);
    }
	
    public void SetHandState(HandStates state)
    {
       // Debug.LogError("state == HandStates.Melee "+ (state == HandStates.Melee));
       // Debug.LogError("state == HandStates.Ranged " + (state == HandStates.Ranged));
       // Melee.gameObject.SetActive(state == HandStates.Melee);
      //  Ranged.gameObject.SetActive(state == HandStates.Ranged);
    }
}
