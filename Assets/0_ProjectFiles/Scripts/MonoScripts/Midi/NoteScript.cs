using MidiPlayerTK;
using System;
using System.Collections;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(Rigidbody))]
public class NoteScript : ANote
{

    private Vector3 hitDirection;
    public LevelSounds levelSounds;
    public FloatVariable SwordSpeed;
    public GameEvent NoteCutE;
    public GameEvent NoteMissE;

    [Space()]
    public GameObject UP;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;
    public GameObject Body;


    private AudioSource audioSource;
    private WaitWhile coroutinrRule;
    private Coroutine cor;
    private int SourceLane;
    private Direction slashDirection;


    void Awake()
    {
        coroutinrRule = new WaitWhile(() => { return audioSource.isPlaying; });
        audioSource = GetComponent<AudioSource>();
    }

    public void Spawn(Vector3 source, Vector3 dist, int lane, Direction slashDirection)
    {
        Spawn(source, dist);
        Body.SetActive(true);
        Rb.GetComponent<Collider>().enabled = true;
        SourceLane = lane;
        this.slashDirection = slashDirection;
        SetDirection(slashDirection);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Sword")
        {
            Co.enabled = false;
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
            Rb.velocity = new Vector3(0, 0, 0);
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
        Co.enabled = false;
        Rb.velocity = new Vector3(0, 0, 0);
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

    public override void Hide()
    {
        NotesPoolScript.inistance.PushNote(transform);
    }



}
