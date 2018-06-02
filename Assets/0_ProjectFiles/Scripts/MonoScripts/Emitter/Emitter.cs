using System;
using UnityEngine;
using UnityEngine.Events;

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

    public TransformVariable bossRightHand;
    public TransformVariable bossLeftHand;
    public TransformVariable bossMeleeHand;

    Vector3 eventCollectorPos;

    public void Awake()
    {
        inistance = this;
    }

    void Start()
    {
        CreateEventNoteTrigger();
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
        var noteAudio = (MidiNoteAudio)data.value;
        SpawnNote(noteAudio);
    }

    int noteIndex;
    void SpawnNote(MidiNoteAudio note)
    {
        // DebugNote(note);

        Direction slashDir = ExtractSlashDir(note);

        Transform source;
        Vector3 distination;
        CalculateNoteDirection(note, out source, out distination);

        if (GetNoteIndex(note) < 7)
        {
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
                source = bossLeftHand.value;
            }
            else
            {
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
        EmitEvent(startBgMusicE);
    }
    public void OnMidiEnd()
    {
        EmitEvent(stopBgMusicE);
    }
    public void EmitEvent(GameEvent gameEvent)
    {
        var eventNote = Instantiate(eventNotePref).GetComponent<EventsNoteScript>();
        // note.ObjectBool = transform;
        eventNote.tag = "Note";
        eventNote.Spawn(EventEmitter.position, eventCollectorPos, gameEvent, this);
    }

}
