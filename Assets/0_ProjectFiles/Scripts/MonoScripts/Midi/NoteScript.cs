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
    public GameEvent NoteMissE;
    public Vector3 velocityBeforePause;
    [Space()]
    public GameObject UP;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;

    public GameObject Body;

    private Rigidbody rb;
    private AudioSource audioSource;
    private WaitWhile coroutinrRule;
    private Coroutine cor;
    private int SourceLane;
    private Direction slashDirection;

    private void Awake()
    {
        coroutinrRule = new WaitWhile(() => { return audioSource.isPlaying; });
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Spawn(Vector3 source, Vector3 dist, int lane, Direction slashDirection)
    {
        transform.SetParent(null);
        Body.SetActive(true);
        rb.GetComponent<Collider>().enabled = true;


        transform.position = source;
    
        var newDistance = Vector3.Distance(source, dist);
        float newVelocity = newDistance / settings.NoteVelocity;
        rb.velocity = (dist - source).normalized * newVelocity;

        SourceLane = lane;
        this.slashDirection = slashDirection;
        SetDirection(slashDirection);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Sword")
        {
            rb.GetComponent<Collider>().enabled = false;
            var swordScript = collision.GetComponent<Sword>();
            if (swordScript.dir == slashDirection && SwordSpeed.value > settings.minCutSpeed)
            {
                OnNoteCut();
            }
            else
            {
                OnNoteWorngCut();
            }
        }
        else
        {
            Hide();
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnNoteWorngCut()
    {
        DestroyNote();
        NoteMissE.Raise();
    }

    private void OnNoteCut()
    {
        DestroyNote();
        NoteCutE.Raise();
    }

    public void DestroyNote()
    {
        Body.SetActive(false);
        rb.GetComponent<Collider>().enabled = false;
        rb.velocity = new Vector3(0, 0, 0);
        SetDirection(Direction.NONE);
        cor = StartCoroutine(PlayNote(levelSounds.LaneSounds[SourceLane], Hide));
    }

    protected IEnumerator PlayNote(AudioClip audio, Action callback)
    {
        audioSource.clip = audio;
        audioSource.Play();
        yield return coroutinrRule;
        callback();
    }

    public void SetDirection(Direction dir)
    {
        UP.SetActive(dir == Direction.UP);
        Down.SetActive(dir == Direction.DOWN);
        Left.SetActive(dir == Direction.LEFT);
        Right.SetActive(dir == Direction.RIGHT);
    }

    internal void OnHide()
    {

    }

    public void Hide()
    {
        NotesPoolScript.inistance.PushNote(transform);
    }

    public void VelocityBeforePause()
    {

        velocityBeforePause = rb.velocity;
        rb.velocity = Vector3.zero;
    }
    public void VelocityAfterPause()
    {
        rb.velocity = velocityBeforePause;

    }


}
