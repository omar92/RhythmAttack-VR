using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{

    public Image bar;
    public FloatVariable remainingTime;

    private void Update()
    {
        bar.fillAmount = remainingTime.value;
    }
  
    private void OnEnable()
    {
        bar.fillAmount = 1;
    }
}

//public class MusicBar : MonoBehaviour
//{

//    //float musicLenght;   //1
//    //float progress;       //x
//    public Image bar;

//    public FloatVariable percentage;
//    void Start()
//    {

//        //musicLenght = musicPlayer.GetComponent<AudioSource>().clip.length;
//       // bar = transform.GetChild(0).gameObject.GetComponentsInChildren<Image>()[0];
//        StartCoroutine(Count());
//    }
//    //private void Update()
//    //{
//    //    musicLenght = musicPlayer.GetComponent<AudioSource>().clip.length;
//    //    //musicLenght = musicPlayer.GetComponent<AudioSource>().clip.length;
//    //    //Debug.Log("valueeeeeeeeee " + musicLenght);
//    //    //bar.fillAmount -= ((100 / musicLenght) / 100)/Time.deltaTime;
//    //    //bar.fillAmount -= .00625f ;
//    //    //progress = FindObjectOfType<BGMusicHandler>().GetComponent<AudioSource>().time;
//    //    //Debug.Log(" Lenght " + progress);
//    //    //bar.fillAmount =progress / musicLenght;
//    //    //  bar.fillAmount = progrsss;
//    //    //Debug.Log(" music  Lenght " + musicLenght);
//    //    //Debug.Log(" music progress " + progress);
//    //}
//    IEnumerator Count()
//    {
//        while (true)
//        {

//            //Debug.Log("  percentage.value " + percentage.value);
//            //Debug.Log("  percentage over 100 " + percentage.value/100);
//            bar.fillAmount -=  percentage.value/100;
//            yield return new WaitForSeconds(1);
//        }

//    }
//    public  void Reset()
//    {
//        bar.fillAmount = 1;
//    }
//}
