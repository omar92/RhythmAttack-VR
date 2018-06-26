using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntroMusicMonitor : MonoBehaviour
{
    public GameEvent introMusicEnd;
    public LevelSettings settings;
    Coroutine waitMusic;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void BeginCoroutine()
    {
        waitMusic = StartCoroutine(MusicCoroutine());
    }
    public void EndCoroutine()
    {
        StopCoroutine(waitMusic);
    }

    IEnumerator MusicCoroutine()
    {
        yield return new WaitForSeconds(source.clip.length - settings.NoteVelocity - settings.ThrowDelay);
        Debug.Log("change state");
        introMusicEnd.Raise();
    }


}
