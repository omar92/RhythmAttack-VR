using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MidiAnalyiser
{

    //public string midi = "Assets/";

    public static int[] GetMidiNotesTypes(string midi)
    {
       // Debug.Log(midi);
        byte[] MIDI = File.ReadAllBytes(midi);

        List<int> midiNotes = new List<int>();

        for (int i = 0; i < MIDI.Length; i++)
        {
            if (MIDI[i] >= 144 && MIDI[i] <= 159)
            {
                if (MIDI[i] != 144)
                    continue;
                if (!midiNotes.Contains(MIDI[i + 1]))
                    midiNotes.Add(MIDI[i + 1]);
            }

            if (MIDI[i] >= 128 && MIDI[i] <= 143)
            {

                if (MIDI[i] != 128)
                    continue;
                if (!midiNotes.Contains(MIDI[i + 1]))
                    midiNotes.Add(MIDI[i + 1]);
            }
        }

        return midiNotes.ToArray();
    }




}
