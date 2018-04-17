using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour {

    public GameObject[] lanes;
    int randNum;
   // Rigidbody rb;
    public Rigidbody pref;


	void Start () {
        StartCoroutine(Throw());
	}
	
	IEnumerator Throw()
    {
        while (true)
        {
            Rigidbody clone;
            randNum = Random.Range(0, lanes.Length - 1);
            clone = Instantiate(pref, lanes[randNum].transform.position, Quaternion.identity) as Rigidbody;
            clone.velocity = new Vector3(0, 0, -20.0f);
            clone.tag = "Minion";
            yield return new WaitForSeconds(1);
        }
    }
}
