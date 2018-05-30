using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesPoolScript : MonoBehaviour
{

    [Header("Note")]
    public NoteScript NotePref;
    //public EventsNoteScript eventNotePref;

    public static NotesPoolScript inistance;


    private void Awake()
    {
        inistance = this;
    }

    public NoteScript PullNote(Vector3 source , Vector3 dist , float originalDistance, int lane,  Direction slashDirection)
    {
        NoteScript noteScript;
        if (transform.childCount > 0)
        {
            noteScript = transform.GetChild(0).GetComponent<NoteScript>();
        }
        else
        {
            noteScript = InstantiateNoteTransform();
        }
        noteScript.Spawn(source,dist,  lane , slashDirection);
        return noteScript;
    }

    public void PushNote(Transform note)
    {
        note.GetComponent<NoteScript>().OnHide();
        note.SetParent(transform);
        note.position = transform.position;
    }

    private NoteScript InstantiateNoteTransform()
    {
        var note = Instantiate(NotePref.transform).GetComponent<NoteScript>();
        // note.ObjectBool = transform;
        note.tag = "Note";
        return note;
    }

    //internal EventsNoteScript PullNote(Vector3 position, EmitterEvents emitterEvent)
    //{
    //    var eventNote = Instantiate(eventNotePref.transform).GetComponent<EventsNoteScript>();
    //    // note.ObjectBool = transform;
    //    eventNote.tag = "Note";
    //    return eventNote;
    //}
}
