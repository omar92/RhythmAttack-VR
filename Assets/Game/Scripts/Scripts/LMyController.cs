using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMyController : MonoBehaviour {

    // Use this for initialization
    GameObject leftController;

    Vector3 currentPos;
    Vector3 previuosPos;
    
    float speed;

   

    // Use this for initialization
    private void Awake()
    {
        leftController = GameObject.Find("LeftController");

        leftController.transform.position = this.transform.position;
        leftController.transform.rotation = this.transform.rotation;
    }
    void Start()
    {
        previuosPos = Vector3.zero;
    }

 

    void Update()
    {
       
        currentPos = transform.position;

        speed = (currentPos - previuosPos).magnitude / Time.deltaTime;

        previuosPos = currentPos;
        Debug.Log("left control speed = " + speed);
    }

 
}