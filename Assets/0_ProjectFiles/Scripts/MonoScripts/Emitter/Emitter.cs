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
        //Debug.Log("note Midi" + note.note.Midi
        //+ "\n" + "note Pitch" + note.note.Pitch
        //+ "\n" + "note Patch" + note.note.Patch
        //+ "\n" + "note AbsoluteQuantize" + note.note.AbsoluteQuantize
        //+ "\n" + "note Chanel" + note.note.Chanel
        //+ "\n" + "note Delay" + note.note.Delay
        //+ "\n" + "note Drum" + note.note.Drum
        //+ "\n" + "note Duration" + note.note.Duration
        //+ "\n" + "note Velocity" + note.note.Velocity);
        // var velocityRatio = (float)(note.note.Velocity * 100) / (125);

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


        laneNum = GlobalData.tracksNotesLanesMaper[(int)currentTrackIndex.value][note.note.Midi];
        var em = GetLane(laneNum);
        NoteScript clone = NotesPoolScript.inistance.PullNote(em.position, laneNum, slashDir);
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

//// <<<<<
//note Midi60
//note Pitch1.002313
//note Patch0
//note AbsoluteQuantize6144
//note Chanel1
//note Delay0
//note DrumFalse
//note Duration300
//note Velocity48

//// <<<<<<
//note Midi60
//note Pitch1.002313
//note Patch0
//note AbsoluteQuantize7680
//note Chanel1
//note Delay0
//note DrumFalse
//note Duration300
//note Velocity47

////>>>>>
//note Midi59
//note Pitch0.9460576
//note Patch0
//note AbsoluteQuantize8064
//note Chanel1
//note Delay0
//note DrumFalse
//note Duration300
//note Velocity15

////<<<<<<<<
//note Midi60
//note Pitch1.002313
//note Patch0
//note AbsoluteQuantize8448
//note Chanel1
//note Delay0
//note DrumFalse
//note Duration300
//note Velocity46

////vvvvvvvvvvvvvvv
//note Midi60
//note Pitch1.002313
//note Patch0
//note AbsoluteQuantize9216
//note Chanel1
//note Delay0
//note DrumFalse
//note Duration300
//note Velocity112

////vvvvvvvvvvvvvvv
//note Midi59
//note Pitch0.9460576
//note Patch0
//note AbsoluteQuantize9600
//note Chanel1
//note Delay0
//note DrumFalse
//note Duration300
//note Velocity112

////^^^^^^^^^^^^^^
//note Midi60
//note Pitch1.002313
//note Patch0
//note AbsoluteQuantize10752
//note Chanel1
//note Delay0
//note DrumFalse
//note Duration300
//note Velocity77

