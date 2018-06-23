using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroMusicMonitor : MonoBehaviour
{

    public FloatVariable musicLengyh;
    public GameEvent introMusicEnd;
    public float EndAt;
    Coroutine waitMusic;

    public void BeginCoroutine()
    {
        waitMusic = StartCoroutine(MusicCoroutine());
    }

    IEnumerator MusicCoroutine()
    {
        while (musicLengyh.value > EndAt)
        {
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("change state");
        introMusicEnd.Raise();
    }
}
