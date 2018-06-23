using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMusicMonitor : MonoBehaviour
{

    public FloatVariable musicLengyh;
    public GameEvent introMusicEnd;
    Coroutine waitMusic;

    public void BeginCoroutine()
    {
        waitMusic = StartCoroutine("MusicCoroutine");
    }

    IEnumerable MusicCoroutine()
    {
        while (musicLengyh.value > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        introMusicEnd.Raise();
    }
}
