using MidiPlayerTK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NoteScript : MonoBehaviour
{

    public float Velocity = 10;

    [Range(0f, 1f)]
    public float MPTK_Volume = 1;

    protected float VolumeTransitionTime = 0.1f;

    [HideInInspector]
    public Transform ObjectBool { get; internal set; }
    private Rigidbody rb;
    private MidiNoteAudio myNoteAudio;
    //  private NoteScript noteScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //noteScript = GetComponent<NoteScript>();
    }

    public void Spawn(Vector3 pos, MidiNoteAudio note)
    {
        // this.enabled = true;
        transform.SetParent(null);
        transform.position = pos;
        rb.GetComponent<Renderer>().enabled = true;
        rb.GetComponent<Collider>().enabled = true;
        rb.velocity = new Vector3(0, 0, -Velocity);
        myNoteAudio = note;
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = new Vector3(0, 0, 0);
        if (other.tag == "Sword" && other.GetComponent<Sword>().speed > 2)
        {
            rb.GetComponent<Renderer>().enabled = false;
            rb.GetComponent<Collider>().enabled = false;
            StartCoroutine(PlayNote(myNoteAudio.audioSource, !myNoteAudio.note.Drum, myNoteAudio.note, Hide));
        }
        else
        {
            Hide();
        }
    }


    /// <summary>
    /// Play one note with this AudioSource
    /// </summary>
    /// <param name="audio">AudioSource</param>
    /// <param name="loop">Sound with loop</param>
    /// <param name="note"></param>
    /// <returns></returns>
    protected IEnumerator PlayNote(AudioSource audio, bool loop, MidiNote note, Action OnEnd)
    {
        if (note.Delay > 0f)
        {
            float endDelay = Time.realtimeSinceStartup + note.Delay / 1000f;
            while (Time.realtimeSinceStartup < endDelay)
                yield return new WaitForSeconds(0.01f);
        }
        try
        {
            audio.pitch = note.Pitch;
            audio.loop = loop;
            //audio.volume = note.Gain;
            audio.volume = Mathf.Lerp(0f, 1f, note.Velocity / 127f) * MPTK_Volume;
            audio.Play();
        }
        catch (Exception)
        {
        }

        // Attack & Decay are taken in account by the wave
        if (loop)
        {
            // Sustain phase until key release, constant amplitude
            float end = Time.realtimeSinceStartup + (float)(note.Duration / 1000d);
            while (true)
            {
                try
                {
                    if (Time.realtimeSinceStartup >= end || !audio.isPlaying)
                        break;
                }
                catch (Exception)
                {
                }
                yield return new WaitForSeconds(0.01f);
            }
            // Release phase
            if (VolumeTransitionTime > 0f)
            {
                float dtime = 0f;
                float volume = 0;

                try
                {
                    volume = audio.volume;
                    end = Time.realtimeSinceStartup + VolumeTransitionTime;

                }
                catch (Exception)
                {
                }

                do
                {
                    dtime = end - Time.realtimeSinceStartup;
                    try
                    {
                        audio.volume = Mathf.Lerp(0f, volume, dtime / VolumeTransitionTime);
                        if (dtime < 0f || !audio.isPlaying)
                            break;

                    }
                    catch (Exception)
                    {
                        break;
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                while (true);

                try
                {
                    audio.Stop();
                }
                catch (Exception)
                {
                }
            }
        }
        //else
        //{
        //    // play with no loop (drum)
        //    while (audio.isPlaying)
        //    {
        //        yield return new WaitForSeconds(0.01f);
        //    }
        //}
        Destroy(audio.gameObject);
        OnEnd();

    }




    internal void OnHide()
    {

    }

    public void Hide()
    {
        NotesPoolScript.inistance.PushNote(transform);
    }
}
