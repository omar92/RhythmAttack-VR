using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{

    float musicLenght;   //1
    float progress;       //x
    public Image bar;

    
    void Start()
    {
        musicLenght = FindObjectOfType<BGMusicHandler>().GetComponent<AudioSource>().clip.length;
       // bar = transform.GetChild(0).gameObject.GetComponentsInChildren<Image>()[0];
        StartCoroutine(Count());
    }
    private void Update()
    {
        musicLenght = FindObjectOfType<BGMusicHandler>().GetComponent<AudioSource>().clip.length;
        //Debug.Log("valueeeeeeeeee " + ((100 / musicLenght) / 100) );
        //bar.fillAmount -= ((100 / musicLenght) / 100)/Time.deltaTime;
        //bar.fillAmount -= .00625f ;
        //progress = FindObjectOfType<BGMusicHandler>().GetComponent<AudioSource>().time;
        //Debug.Log(" Lenght " + progress);
        //bar.fillAmount =progress / musicLenght;
        //  bar.fillAmount = progrsss;
        //Debug.Log(" music  Lenght " + musicLenght);
        //Debug.Log(" music progress " + progress);
    }
    IEnumerator Count()
    {
        while (true)
        {
            Debug.Log(" IEnumerator working ");
            bar.fillAmount -=  (100 / musicLenght) / 100;
            yield return new WaitForSeconds(1);
        }

    }
}
