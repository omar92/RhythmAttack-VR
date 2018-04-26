using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour {

    static float playerHealth=100;
    
	public static void hitPlayer(float damage)
    {
        playerHealth -= damage;
        if (playerHealth<=0)
        {
           //lose scene
        }
       
    }
}
