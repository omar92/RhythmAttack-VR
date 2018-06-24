//using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Emitter : MonoBehaviour
{
    public Transform EventEmitter;
    public EventsNoteScript eventNotePref;
    public EvadeNoteScript evadeNotePref;
    public TransformVariable player;
    public static Emitter inistance = null;

    public FloatVariable currentTrackIndex;

    [Header("GameEvents")]
    public GameEvent startBgMusicE;
    public GameEvent stopBgMusicE;
    public GameEvent attakEndE;

    public GameEvent ThrowFromLeftHand;
    public GameEvent ThrowFromRightHand;

    public GameEvent TutorialDone;
    public GameEvent TutorialGun;
    [Header("Boss Hands")]
    public TransformVariable bossRightHand;
    public TransformVariable bossLeftHand;
    public TransformVariable bossMeleeHand;

    EmitterMode mode = EmitterMode.Defence;

    Vector3 eventCollectorPos;

    GameObject[] AttackTargets;
    List<GameObject> ActiveAttackTargets = new List<GameObject>();

    public void Awake()
    {
        inistance = this;
        AttackTargets = GameObject.FindGameObjectsWithTag("Targets");
    }

    void Start()
    {
        CreateEventNoteTrigger();
        HideAttackTargets();

    }


    public void SetAttackMode()
    {
        mode = EmitterMode.Attack;
    }

    Coroutine TutorialCo;
    int TutorialProgrees;
    bool TutorialProceed;
    public void StartTutorial()
    {
        mode = EmitterMode.Tutorial;

        TutorialCo = StartCoroutine(TutorialCoFun());

    }
    public void StopTutorial()
    {
        StopCoroutine(TutorialCo);

    }

    bool isTutorialFinished = false;
    IEnumerator TutorialCoFun()
    {
        TutorialProgrees = 0;
        TutorialProceed = false;
        isTutorialFinished = false;
        var midiMidle = 0;
        foreach (var midi in GlobalData.DefenceTracksNotesIndicies[(int)currentTrackIndex.value].Keys)
        {
            if (GlobalData.DefenceTracksNotesIndicies[(int)currentTrackIndex.value][midi] == 2)
            {
                midiMidle = midi;
                break;
            }
        }

        var noteAudio = new MidiNoteAudio
        {
            note = new MidiPlayerTK.MidiNote
            {
                Midi = midiMidle
            }
        };

        while (!isTutorialFinished)
        {
            switch (TutorialProgrees)
            {
                case 0:
                    noteAudio.note.Velocity = 100;
                    SpawnNote(noteAudio);
                    break;
                case 1:
                    noteAudio.note.Velocity = 10;
                    SpawnNote(noteAudio);
                    break;
                case 2:
                    noteAudio.note.Midi = midiMidle;
                    noteAudio.note.Velocity = 10;
                    SpawnNote(noteAudio, true);
                    break;
                case 3:
                    noteAudio.note.Midi = midiMidle;
                    noteAudio.note.Velocity = 100;
                    SpawnNote(noteAudio, true);
                    break;
                case 4:
                    TutorialGun.Raise();
                    TutorialProgrees++;
                    TutorialProceed = true;
                    break;
                case 5:
                case 6:
                    ActivateRangedTarget();
                    TutorialProceed = true;
                    yield return new WaitForSeconds(2);
                    break;
                default:
                    isTutorialFinished = true;
                    TutorialProceed = true;
                    break;
            }
            while (!TutorialProceed)
            {
                yield return new WaitForEndOfFrame();
            }
            TutorialProceed = false;
            // yield return new WaitForEndOfFrame();
        }
        TutorialDone.Raise();
    }

    public void OnTutorialSuccess()
    {
        TutorialProgrees++;
        TutorialProceed = true;
    }

    public void OnTutorialFail()
    {
        TutorialProceed = true;
    }
    public void HideAttackTargets()
    {
        for (int i = 0; i < AttackTargets.Length; i++)
        {
            AttackTargets[i].SetActive(false);
        }
    }

    private void CreateEventNoteTrigger()
    {
        eventCollectorPos = EventEmitter.position;
        eventCollectorPos.z = GetZOfNotesReach();
        var emitter = GameObject.Instantiate(EventEmitter, eventCollectorPos, Quaternion.identity);
        emitter.gameObject.AddComponent<EmitterEventsCollector>();
        emitter.GetComponent<Collider>().enabled = true;
    }

    private float GetZOfNotesReach()
    {
        return player.value.position.z + (player.value.lossyScale.z);
    }



    public void OnMidiNoteAudio(ObjectVariable data)
    {
        switch (mode)
        {
            case EmitterMode.Defence:
                var noteAudio = (MidiNoteAudio)data.value;
                SpawnNote(noteAudio);
                break;
            case EmitterMode.Attack:
                ActivateRangedTarget();
                break;
            default:
                break;
        }

    }

    private void ActivateRangedTarget()
    {
        StartCoroutine(ShowTargetForTime(2f));
    }

    private IEnumerator ShowTargetForTime(float time)
    {
        int traials = 10;
        var target = AttackTargets[Random.Range(0, AttackTargets.Length)];// AttackTargets.get
        while (target.activeInHierarchy && --traials >= 0)
        {
            yield return true;
            target = AttackTargets[Random.Range(0, AttackTargets.Length)];// AttackTargets.get
        }
        if (traials > 0)
        {
            ActiveAttackTargets.Add(target);
            target.SetActive(true);
            yield return new WaitForSeconds(time);
            target.SetActive(false);
            ActiveAttackTargets.Remove(target);
        }
    }

    int noteIndex;
    void SpawnNote(MidiNoteAudio note, bool ForceEvade = false)
    {
        // DebugNote(note);

        Direction slashDir = ExtractSlashDir(note);

        Transform source;
        Vector3 distination;
        CalculateNoteDirection(note, out source, out distination);

        if (GetNoteIndex(note) >= 7 || ForceEvade)
        {
            distination.z = player.value.position.z;
            SpawnEvade(source, distination, slashDir);
        }
        else
            NotesPoolScript.inistance.PullNote(source.position, distination, noteIndex, slashDir);

    }

    private void SpawnEvade(Transform source, Vector3 distination, Direction slashDir)
    {
        var eventNote = Instantiate(evadeNotePref).GetComponent<EvadeNoteScript>();
        // note.ObjectBool = transform;
        eventNote.tag = "Evade";
        eventNote.Spawn(source.position, distination, slashDir);
    }

    private static void DebugNote(MidiNoteAudio note)
    {
        Debug.Log("note Midi" + note.note.Midi
      + "\n" + "note Pitch" + note.note.Pitch
      + "\n" + "note Patch" + note.note.Patch
      + "\n" + "note AbsoluteQuantize" + note.note.AbsoluteQuantize
      + "\n" + "note Chanel" + note.note.Chanel
      + "\n" + "note Delay" + note.note.Delay
      + "\n" + "note Drum" + note.note.Drum
      + "\n" + "note Duration" + note.note.Duration
      + "\n" + "note Velocity" + note.note.Velocity);
        var velocityRatio = (float)(note.note.Velocity * 100) / (125);
    }

    private void CalculateNoteDirection(MidiNoteAudio note, out Transform source, out Vector3 distination)
    {
        var laneCords = GetLanCords(note);
        if (laneCords.y == 2)
        {
            source = bossMeleeHand.value;
        }
        else
        {
            if (laneCords.x < 2)
            {
                ThrowFromLeftHand.Raise();
                source = bossLeftHand.value;
            }
            else
            {
                ThrowFromRightHand.Raise();
                source = bossRightHand.value;
            }
        }
        distination = GetLane(laneCords).position;
        distination.z = GetZOfNotesReach();
    }

    private static Direction ExtractSlashDir(MidiNoteAudio note)
    {
        Direction slashDir;
        if (note.note.Velocity >= 0 && note.note.Velocity < 31.5)//25%
        {
            slashDir = Direction.RIGHT;
        }
        else if (note.note.Velocity >= 31 && note.note.Velocity < 62.5)//50%
        {
            slashDir = Direction.LEFT;
        }
        else if (note.note.Velocity >= 62.5 && note.note.Velocity < 93.75)//75%
        {
            slashDir = Direction.UP;
        }
        else //100%
        {
            slashDir = Direction.DOWN;
        }

        return slashDir;
    }

    Transform GetLane(Vector2 cord)
    {
        return transform.GetChild((int)cord.y).GetChild((int)cord.x);
    }
    Transform GetLane(MidiNoteAudio note)
    {
        return GetLane(GetLanCords(note));
    }

    Vector2 GetLanCords(MidiNoteAudio note)
    {
        int index = GetNoteIndex(note);
        return new Vector2(index % transform.GetChild(0).childCount, (index / (transform.GetChild(0).childCount)));
    }

    private int GetNoteIndex(MidiNoteAudio note)
    {
        return GlobalData.DefenceTracksNotesIndicies[(int)currentTrackIndex.value][note.note.Midi];
    }

    public void StartEmitiing()
    {
        mode = EmitterMode.Defence;
        EmitEvent(startBgMusicE);
    }
    public void OnMidiEnd()
    {
        if (mode == EmitterMode.Defence)
        {
            print("Defence End");
            EmitEvent(stopBgMusicE);
        }
        else
        {
            print("Attack End");
            StartCoroutine(StartAfterDelay(2f, () => { attakEndE.Raise(); }));

        }
    }

    private IEnumerator StartAfterDelay(float v, System.Action p)
    {
        yield return new WaitForSeconds(v);
        p();
    }

    private void EmitEvent(GameEvent gameEvent)
    {
        var eventNote = Instantiate(eventNotePref).GetComponent<EventsNoteScript>();
        // note.ObjectBool = transform;
        eventNote.tag = "Note";
        eventNote.Spawn(EventEmitter.position, eventCollectorPos, gameEvent, this);
    }

    enum EmitterMode
    {
        Defence, Attack, Tutorial
    }
}
