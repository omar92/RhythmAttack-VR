using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPercentage : MonoBehaviour
{

    public FloatVariable remainingTime;
    AudioSource source;
    float passedTime;
    bool bossStamina=false;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (bossStamina)
        {
            passedTime += Time.deltaTime;
            remainingTime.value = (source.clip.length - passedTime) / source.clip.length;
        }
        
    }
    public void StartBossStamina()
    {
        passedTime = 0;
        remainingTime.value = 1f;
        bossStamina = true;
    }
    public void EndBossStamina()
    {
        bossStamina = false;
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
