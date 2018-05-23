using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardHandler : MonoBehaviour {

    public GameEvent doneEditing;

    public void DoneEditing( string myText)
    {
        doneEditing.Raise();
    }
}
