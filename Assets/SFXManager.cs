using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {
    
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] hitNoteClips;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CurrentClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }
    public void RandomizeHitClip()
    {
        int randomClip = Random.Range(0, hitNoteClips.Length - 1);
        CurrentClip(hitNoteClips[randomClip]);
    }
}
