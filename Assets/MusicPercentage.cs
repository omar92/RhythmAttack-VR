using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPercentage : MonoBehaviour {

    public FloatVariable percentage;
    
    
    AudioSource source;
    AudioClip clip;
	void Start () {
        source = GetComponent<AudioSource>();
        clip = source.clip;
	}
	
	
	void Update () {

        percentage.value = Time.time / clip.length;
        
	}
}
