using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardHandler : MonoBehaviour {

    public GameEvent doneEditing;
    public Text myText;
    public StringVariable playerName;

    public void DoneEditing( )
    {
        playerName.value = myText.text;
        Debug.Log("==========> "+playerName.value);
        doneEditing.Raise();
    }
}
