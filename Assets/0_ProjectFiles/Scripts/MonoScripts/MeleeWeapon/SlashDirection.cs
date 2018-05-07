﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDirection : MonoBehaviour {

    public  Vector3Variable hitDirection;

    private Vector3 collisionPoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            collisionPoint = collision.contacts[0].point;
            hitDirection.value = SwordDirection(collisionPoint);
<<<<<<< HEAD
            Debug.Log("this is x "+hitDirection.value.x+ " this is y " + hitDirection.value.y);
=======
>>>>>>> 96f8021679a5c4852375a2abae69c6829584affe
        }
    }


    Vector3 SwordDirection(Vector3 collisionPoint)
    {
        return (collisionPoint - transform.position).normalized;
<<<<<<< HEAD
        
    }

   
=======
    }


>>>>>>> 96f8021679a5c4852375a2abae69c6829584affe
}
