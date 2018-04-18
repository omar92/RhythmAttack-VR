using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{

    public GameObject[] lanes;
    public float speedIncrease = .02f;
    int randNum;

    //[Range(.2f, 3)]
    public float speedRate = -1;
    // Rigidbody rb;
    public Rigidbody pref;


    void Start()
    {
        StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {
        var track = Utilities.track;
        speedRate = 0;
        while (true)
        {
            //int rand = Random.Range(0, 2);

            //for (int i = 0; i <= rand; i++)
            //{
            //    Rigidbody clone;
            //    randNum = Random.Range(0, lanes.Length);
            //    clone = Instantiate(pref, lanes[randNum].transform.position, Quaternion.identity) as Rigidbody;
            //    clone.velocity = new Vector3(0, 0, -20.0f);
            //    clone.tag = "Minion";

            //}

            for (int y = 0; y < track.Length; y += 5)
            {
                var newSpeed = track[y + 4]*2f - speedRate;
                yield return new WaitForSeconds(newSpeed > .1f ? newSpeed : .1f);
                for (int x = y; x < y + 4; x++)
                {
                    if (track[x] > 0)
                    {
                        var em = GetEmitter(0, x % 5);
                        Rigidbody clone;
                        //randNum = Random.Range(0, lanes.Length);
                        clone = Instantiate(pref, em.transform.position, Quaternion.identity) as Rigidbody;
                        clone.velocity = new Vector3(0, 0, -20.0f);
                        clone.tag = "Minion";
                    }
                }
            }
            speedRate += speedIncrease;
            print("Track Done");
        }
    }


    Transform GetEmitter(int row, int col)
    {
        return transform.GetChild(row).GetChild(col);
    }
}


