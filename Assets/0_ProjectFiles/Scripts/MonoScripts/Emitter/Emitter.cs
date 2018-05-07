using UnityEngine;
using UnityEngine.Events;

public class Emitter : MonoBehaviour
{
    public Transform EventEmitter;
    public EventsNoteScript eventNotePref;
    public static Emitter inistance = null;
    

    [Header("GameEvents")]
    public GameEvent startBgMusicE;
    public GameEvent stopBgMusicE;
   // public UnityEvent OnDone;

    public void Awake()
    {
        inistance = this;

        var eventCollectorPos = EventEmitter.position;
        var player = GameObject.FindGameObjectWithTag("Player");
        eventCollectorPos.z = player.transform.position.z;
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


    void SpawnNote(MidiNoteAudio note)
    {
        var em = GetLane(GlobalData.tracksNotesLanesMaper[0][note.note.Midi]);
        NoteScript clone = NotesPoolScript.inistance.PullNote(em.position, note);
    }


    Transform GetLane(int row, int col)
    {
        return transform.GetChild(row).GetChild(col);
    }
    Transform GetLane(int index)
    {
        return GetLane((index / (transform.GetChild(0).childCount)), index);
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

