using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDirection : MonoBehaviour
{

    private Vector3 positionOnTrigger;

    Vector2 hitDirection;
    
    Vector3 secondHitPoint;

    Vector2 precviousPosition;
    Vector2 currentPosition;

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Sword")
        {

            currentPosition = new Vector2(other.transform.position.x,other.transform.position.y);
            precviousPosition = other.GetComponent<Sword>().xyPositions[0];
            hitDirection = (currentPosition - precviousPosition).normalized;

            if (Vector2.Dot(hitDirection,transform.up)<0)
            {
                Debug.Log(" you are hitting from above")
            }
            //positionOnTrigger = cam.InverseTransformPoint(other.transform.position);
            //hitDirection = (transform.position - positionOnTrigger).normalized;
            //secondHitPoint = hitDirection * multiplcationScale.x;


            //Debug.Log("positionOnTrigger " + positionOnTrigger);
            //Debug.Log("hitDirection " + hitDirection);
            //Debug.Log("secondHitPoint " + secondHitPoint);
            //Debug.Log(" multiplcationScale.x " + multiplcationScale.x);
            //Debug.Log(" multiplcationScale.y " + multiplcationScale.y);



        }
    }





}
