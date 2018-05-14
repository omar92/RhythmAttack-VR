using MidiPlayerTK;
using System;
using System.Collections;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(Rigidbody))]
public class NoteScript : MonoBehaviour
{

    private Vector3 hitDirection;
    public LevelSettings settings;
    public LevelSounds levelSounds;
    public FloatVariable SwordSpeed;
    public GameEvent NoteCutE;
    public int SourceLane;


    private Rigidbody rb;
    private AudioSource audioSource;
    private WaitWhile coroutinrRule;
    private Coroutine cor;

    private void Awake()
    {
        coroutinrRule = new WaitWhile(() => { return audioSource.isPlaying; });
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }



    public void Spawn(Vector3 pos, int lane)
    {
        // this.enabled = true;
        transform.SetParent(null);
        transform.position = pos;
        rb.GetComponent<Renderer>().enabled = true;
        rb.GetComponent<Collider>().enabled = true;
        rb.velocity = new Vector3(0, 0, -settings.NoteVelocity);
        SourceLane = lane;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Sword")
        {
            if (SwordSpeed.value > settings.minCutSpeed)
            {
                OnNoteCut();

            }
            else
            {
                //sebo fe 7alo 
            }
        }
        else
        {
            Hide();
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnNoteCut()
    {
        Debug.Log("OnNoteCut");
        NoteCutE.Raise();
        rb.GetComponent<Renderer>().enabled = false;
        //rb.GetComponent<Collider>().enabled = false;
        cor = StartCoroutine(PlayNote(levelSounds.LaneSounds[SourceLane], Hide));
        rb.velocity = new Vector3(0, 0, 0);
    }

    protected IEnumerator PlayNote(AudioClip audio, Action callback)
    {
        // Debug.Log("co started");
        audioSource.clip = audio;
        audioSource.Play();
        yield return coroutinrRule;
        callback();
        //  Debug.Log("co ended");
    }

    internal void OnHide()
    {

    }

    public void Hide()
    {
        NotesPoolScript.inistance.PushNote(transform);
    }
}
