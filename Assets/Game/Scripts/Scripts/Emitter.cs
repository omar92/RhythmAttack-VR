using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{

    void OnMidiNoteAudio(object data)
    {
        var noteAudio = (MidiNoteAudio)data;
        SpawnNote(noteAudio);

    }
    int x, y;
    void SpawnNote(MidiNoteAudio note)
    {
        var em = GetLane(x++ % 2, y++ % 4);
        NoteScript clone = NotesPoolScript.inistance.PullNote(em.position, note);
        //randNum = Random.Range(0, lanes.Length);
        // clone = Instantiate(NoteTemp, em.transform.position, Quaternion.identity) as Rigidbody;
        // clone.velocity = new Vector3(0, 0, -NoteVelocity);
        // clone.tag = "Note";
    }



    Transform GetLane(int row, int col)
    {
        return transform.GetChild(row).GetChild(col);
    }
}
//{
//    public GameObject[] lanes;
//    public float speedIncrease = .02f;
//    int randNum;
//    public float ProjectileSpeed = 10.0f;
//    //[Range(.2f, 3)]
//    public float speedRate = -1;
//    // Rigidbody rb;
//    public Rigidbody pref;
//    //--------------------------------------------

//    float[] track;
//    int trackLine = 0;
//    void Start()
//    {
//        track = Utilities.track;
//       // StartCoroutine(Throw());
//    }

//    IEnumerator Throw()
//    {
//       // float[] track = Utilities.track;
//        speedRate = 0;
//        while (true)
//        {
//            //int rand = Random.Range(0, 2);

//            //for (int i = 0; i <= rand; i++)
//            //{
//            //    Rigidbody clone;
//            //    randNum = Random.Range(0, lanes.Length);
//            //    clone = Instantiate(pref, lanes[randNum].transform.position, Quaternion.identity) as Rigidbody;
//            //    clone.velocity = new Vector3(0, 0, -20.0f);
//            //    clone.tag = "Minion";

//            //}

//            for (int y = 0; y < track.Length; y += 5)
//            {
//                var newSpeed = track[y + 4] * 2f - speedRate;
//                yield return new WaitForSeconds(newSpeed > .1f ? newSpeed : .1f);
//                for (int x = y; x < y + 4; x++)
//                {
//                    if (track[x] > 0)
//                    {
//                        var em = GetEmitter(0, x % 5);
//                        Rigidbody clone;
//                        //randNum = Random.Range(0, lanes.Length);
//                        clone = Instantiate(pref, em.transform.position, Quaternion.identity) as Rigidbody;
//                        clone.velocity = new Vector3(0, 0, -ProjectileSpeed);
//                        clone.tag = "Minion";
//                    }
//                }
//            }
//            speedRate += speedIncrease;
//            print("Track Done");
//        }
//    }

//    void OnMidiWave(object data)
//    {
//      //  print("OnMidiWave");
//        var midiWave = (MidiWave)data;

//        if( trackLine>= track.Length)
//        {
//            print("Track Done");
//            //  speedRate += speedIncrease;
//            trackLine = 0;
//        }
//        //throw track line
//        {
//          //  var newSpeed = track[trackLine + 4] * 2f - speedRate;
//           // yield return new WaitForSeconds(newSpeed > .1f ? newSpeed : .1f);
//            //for (int x = trackLine; x < trackLine + 4; x++)
//            //{
//            //    if (track[x] > 0)
//            //    {
//                 //   var em = GetEmitter(0, x % 5);
//                    var em = GetEmitter(0, 0);
//                    Rigidbody clone;
//                    //randNum = Random.Range(0, lanes.Length);
//                    clone = Instantiate(pref, em.transform.position, Quaternion.identity) as Rigidbody;
//                    clone.velocity = new Vector3(0, 0, -ProjectileSpeed);
//                    clone.tag = "Minion";

//                    SphereControll sc = clone.GetComponent<SphereControll>();
//                    sc.MidiWave = midiWave;
//            //    }
//            //}
//            //trackLine += 5;
//        }


//    }
//    Transform GetEmitter(int row, int col)
//    {
//        return transform.GetChild(row).GetChild(col);
//    }
//}


