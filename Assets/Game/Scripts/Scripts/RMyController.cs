using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMyController : MonoBehaviour {

    GameObject rightController;

    Vector3 currentPos;
    Vector3 previuosPos;

    float speed;

    // Use this for initialization
    private void Awake()
    {
        rightController = GameObject.Find("RightController");

        rightController.transform.position = this.transform.position;
        rightController.transform.rotation = this.transform.rotation;
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
        Debug.Log("right control speed = "+speed);

       
        
    }
}