using MidiPlayerTK;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    MidiFilePlayer midiPLayer;
    void Awake()
    {
        var midiPLayer = gameObject.GetComponentInChildren<MidiFilePlayer>();
        if (midiPLayer)
        {
            midiPLayer.enabled = false;
            MidiPlayerInitialiser.Init("Assets/Game/Resources/Tracks/");
            midiPLayer.enabled = true;
            midiPLayer.MPTK_Play();
        }


    }


}
