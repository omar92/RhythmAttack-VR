using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDirection : MonoBehaviour
{



    private Vector3 positionOnTrigger;



    Vector3 hitDirection;
    Vector3 multiplcationScale;
    Vector3 secondHitPoint;

    public Transform cam;


    void Start()
    {
        cam = Camera.main.transform;
        multiplcationScale = GetComponent<BoxCollider>().size * 4;

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Sword")
        {

            positionOnTrigger = cam.InverseTransformPoint(other.transform.position);
            hitDirection = (transform.position - positionOnTrigger).normalized;
            secondHitPoint = hitDirection * multiplcationScale.x;


            Debug.Log("positionOnTrigger " + positionOnTrigger);
            Debug.Log("hitDirection " + hitDirection);
            Debug.Log("secondHitPoint " + secondHitPoint);
            Debug.Log(" multiplcationScale.x " + multiplcationScale.x);
            Debug.Log(" multiplcationScale.y " + multiplcationScale.y);


        }
    }





}
