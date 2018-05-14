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
    public FloatVariable SwordSpeed;
    public GameEvent ballCut;

    [Range(0f, 1f)]
    public float MPTK_Volume = 1;

    protected float VolumeTransitionTime = 0.1f;

    [HideInInspector]
    public Transform ObjectBool { get; internal set; }
    private Rigidbody rb;
    private MidiNoteAudio myNoteAudio;
    private Vector3 collisionPoint;
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
        rb.velocity = new Vector3(0, 0, -settings.NoteVelocity);
        myNoteAudio = note;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Sword")
    //    {
    //        collisionPoint = collision.contacts[0].point;
    //        hitDirection = SwordDirection(collisionPoint);
    //    }
    //}
    //Vector3 SwordDirection(Vector3 collisionPoint)
    //{
    //    return (collisionPoint - transform.position).normalized;
    //}
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Sword")
        {
            if (SwordSpeed.value > settings.minCutSpeed)
            {
                ballCut.Raise();
                rb.GetComponent<Renderer>().enabled = false;
                rb.GetComponent<Collider>().enabled = false;
                StartCoroutine(PlayNote(myNoteAudio.audioSource, !myNoteAudio.note.Drum, myNoteAudio.note, Hide));
                rb.velocity = new Vector3(0, 0, 0);
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
