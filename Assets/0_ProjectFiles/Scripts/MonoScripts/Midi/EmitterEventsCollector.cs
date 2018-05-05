using UnityEngine;
using System.Collections;

public class EmitterEventsCollector : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        EventsNoteScript eventScript = other.GetComponent<EventsNoteScript>();
        if (eventScript)
        {
           // Emitter.inistance.OnReciveEvent(eventScript.emitterEvent);
            Destroy(other.gameObject);
        }
    }
}
