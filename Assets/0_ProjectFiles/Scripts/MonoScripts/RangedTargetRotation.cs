using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedTargetRotation : MonoBehaviour {

    public Vector3 angle = new Vector3(0, 90, -35);
    GameObject player;
	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
        transform.Rotate(angle);
	}
}
