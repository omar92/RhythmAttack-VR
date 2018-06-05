using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSword : MonoBehaviour {
    public GameObject mySword;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = mySword.transform.position;
        transform.rotation = mySword.transform.rotation;
	}
}
