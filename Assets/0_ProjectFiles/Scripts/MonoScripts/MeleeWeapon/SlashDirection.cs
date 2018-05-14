using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDirection : MonoBehaviour {

    public  Vector3Variable swordPosition;

    private Vector3 positionOnTrigger;
    private Vector3 positionBeforeTrigger;


    Vector3 hitDirection;
    Vector3 multiplcationScale;
    Vector3 secondHitPoint;

    public Transform cam;
    public Vector3 localPoint;

    void Start()
    {
        cam = Camera.main.transform;
        localPoint = cam.InverseTransformPoint(transform.position);

        multiplcationScale = GetComponent<BoxCollider>().size;

    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.tag == "Sword")
        {
            positionBeforeTrigger = cam.InverseTransformPoint(swordPosition.value);
            positionOnTrigger = cam.InverseTransformPoint(other.transform.position);
            hitDirection = (positionBeforeTrigger - positionBeforeTrigger).normalized;
            secondHitPoint = hitDirection * multiplcationScale.x;

            //Debug.Log("positionBeforeTrigger "+positionBeforeTrigger);
            //Debug.Log("positionOnTrigger "+positionOnTrigger);
            //Debug.Log("hitDirection " + hitDirection);
        }
    }


    Vector3 SwordDirection(Vector3 collisionPoint)
    {
        return (collisionPoint - transform.position).normalized;
    }



}
