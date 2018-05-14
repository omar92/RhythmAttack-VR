﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDirection : MonoBehaviour {

    public  Vector3Variable swordPosition;

    private Vector3 positionOnTrigger;
    private Vector3 positionBeforeTrigger;


    Vector3 hitDirection;
    Vector3 multiplcationScale;

    private void Start()
    {
        multiplcationScale = GetComponent<BoxCollider>().size;
    }
    private void OnTriggerEnter(Collider other)
    {
        positionBeforeTrigger = swordPosition.value;
        
        if (other.tag == "Sword")
        {
            positionOnTrigger = other.transform.position;



             
            //firstcollisionPoint = other.contacts[0].point;
            //secondcollisionPoint= collision.contacts[collision.contacts.Length-1].point;

            //Debug.Log("first collision point x "+firstcollisionPoint.x+" second collision point y "+ secondcollisionPoint.y);
            ////hitDirection.value = SwordDirection(collisionPoint);
        }
    }


    Vector3 SwordDirection(Vector3 collisionPoint)
    {
        return (collisionPoint - transform.position).normalized;
    }



}
