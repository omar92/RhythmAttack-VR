using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDirection : MonoBehaviour {

    public  Vector3Variable hitDirection;

    private Vector3 firstcollisionPoint;
    private Vector3 secondcollisionPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            //firstcollisionPoint = other.contacts[0].point;
            //secondcollisionPoint= collision.contacts[collision.contacts.Length-1].point;

            //Debug.Log("first collision point x "+firstcollisionPoint.x+" second collision point y "+ secondcollisionPoint.y);
            ////hitDirection.value = SwordDirection(collisionPoint);
        }
    }


    //Vector3 SwordDirection(Vector3 collisionPoint)
    //{
    //    return (collisionPoint - transform.position).normalized;
    //}

    

}
