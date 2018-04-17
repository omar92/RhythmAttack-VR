using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour {

    public GameObject[] lanes;
    int randNum;

    [Range(.2f,3)]
    public float speed = 1;
   // Rigidbody rb;
    public Rigidbody pref;


	void Start () {
        StartCoroutine(Throw());
	}
	
	IEnumerator Throw()
    {
        while (true)
        {
            int rand = Random.Range(0, 2);

            for (int i = 0; i <= rand; i++)
            {
                Rigidbody clone;
                randNum = Random.Range(0, lanes.Length);
                clone = Instantiate(pref, lanes[randNum].transform.position, Quaternion.identity) as Rigidbody;
                clone.velocity = new Vector3(0, 0, -20.0f);
                clone.tag = "Minion";
              
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
