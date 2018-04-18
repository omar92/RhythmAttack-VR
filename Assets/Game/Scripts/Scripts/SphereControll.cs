using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControll : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
