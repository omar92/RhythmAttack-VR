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

    [Space()]
    public GameObject UP;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;


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

    public void Spawn(Vector3 pos, int lane, Direction slashDirection)
    {
        // this.enabled = true;
        transform.SetParent(null);
        transform.position = pos;
        rb.GetComponent<Renderer>().enabled = true;
        rb.GetComponent<Collider>().enabled = true;
        rb.velocity = new Vector3(0, 0, -settings.NoteVelocity);
        SourceLane = lane;
        this.slashDirection = slashDirection;
        SetDirection(slashDirection);
    }

    private void OnTriggerEnter(Collider collision)
    {
      
        if (collision.tag == "Sword")
        {
            var swordScript = collision.GetComponent<Sword>();
          //  Debug.Log("swordScript.dir :" + swordScript.dir + " :::: slashDirection: " + slashDirection);
            if (swordScript.dir== slashDirection && SwordSpeed.value > settings.minCutSpeed)
            {
                OnNoteCut();
            }
            else
            {
                NoteMissE.Raise();
             //   DestroyNote();
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
        //Debug.Log("OnNoteCut");
        NoteCutE.Raise();
        DestroyNote();
    }

    public void DestroyNote()
    {
        rb.GetComponent<Renderer>().enabled = false;
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
}
