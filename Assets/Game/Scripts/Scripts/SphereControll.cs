using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControll : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        print("triggered "+ other.tag);
        if (other.tag == "Sword")
        {
           // this.GetComponent<BoxCollider>().enabled = false;
            var coliders = this.GetComponentsInChildren<BoxCollider>();
            print(coliders);
            coliders[1].enabled = false;

        }
    }
}
