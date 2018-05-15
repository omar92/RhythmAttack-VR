using UnityEngine;
using UnityEngine.Events;

public class Emitter : MonoBehaviour
{
    public Transform EventEmitter;
    public EventsNoteScript eventNotePref;
    public TransformVariable player;
    public static Emitter inistance = null;

    public FloatVariable currentTrackIndex;

    [Header("GameEvents")]
    public GameEvent startBgMusicE;
    public GameEvent stopBgMusicE;
    // public UnityEvent OnDone;

    public void Awake()
    {
        inistance = this;
    }

    void Start()
    {
        var eventCollectorPos = EventEmitter.position;
        eventCollectorPos.z = player.value.position.z;
        var emitter = GameObject.Instantiate(EventEmitter, eventCollectorPos, Quaternion.identity);
        emitter.gameObject.AddComponent<EmitterEventsCollector>();
        emitter.GetComponent<Collider>().enabled = true;

    }
    public void StartEmitiing()
    {
        EmitEvent(startBgMusicE);
    }

    public void OnMidiNoteAudio(ObjectVariable data)
    {
        var noteAudio = (MidiNoteAudio)data.value;
        //  Debug.Log(noteAudio.note.Midi);
        SpawnNote(noteAudio);
    }

    int laneNum;
    void SpawnNote(MidiNoteAudio note)
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

        laneNum = GlobalData.tracksNotesLanesMaper[(int)currentTrackIndex.value][note.note.Midi];
        var em = GetLane(laneNum);
        NoteScript clone = NotesPoolScript.inistance.PullNote(em.position, laneNum);
    }


    Transform GetLane(int row, int col)
    {
        return transform.GetChild(row).GetChild(col);
    }
    Transform GetLane(int index)
    {
        return GetLane((index / (transform.GetChild(0).childCount)), index % transform.GetChild(0).childCount);
    }

    public void EmitEvent(GameEvent gameEvent)
    {
        var eventNote = Instantiate(eventNotePref).GetComponent<EventsNoteScript>();
        // note.ObjectBool = transform;
        eventNote.tag = "Note";
        eventNote.Spawn(EventEmitter.position, gameEvent, this);
    }

    public void OnMidiEnd()
    {
        EmitEvent(stopBgMusicE);
    }
}

