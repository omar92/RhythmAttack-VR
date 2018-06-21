using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPercentage : MonoBehaviour
{

    public FloatVariable remainingTime;
    AudioSource source;
    float passedTime;
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    void Update()
    {
        passedTime += Time.deltaTime;
        remainingTime.value = (source.clip.length - passedTime) / source.clip.length;
    }
}

//public class MusicPercentage : MonoBehaviour {

//    public FloatVariable percentage;


//    AudioSource source;
//    AudioClip clip;
//	void Start () {
//        source = GetComponent<AudioSource>();
//        clip = source.clip;
//	}


//	void Update () {

//        percentage.value = Time.time / clip.length;

//	}
//}
