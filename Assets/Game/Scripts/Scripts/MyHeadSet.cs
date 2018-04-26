using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHeadSet : MonoBehaviour {

    GameObject headSet;

    // Use this for initialization
    private void Awake()
    {
        headSet = GameObject.Find("HeadSet");
        
        headSet.transform.position = this.transform.position;
        headSet.transform.rotation = this.transform.rotation;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
