using System.Collections;
using System.Collections.Generic;
public class MidiAnalyiser{

    public string midi = "Assets/";

    uint Counter = 0;
    byte commandByte = 0;
    byte noteByte = 0;
    byte velocityByte = 0;

    byte noteOn = 144;
    byte[] MIDI;

    int Index = 0;
    int Length = 0;

    public bool PrintInfo = false;
    public bool Channels = true;
    public bool NotesInHex = false;
    public bool OnlyChannelOne = false;

    int AbsoluteQuantize;

    public static int[] GetMidiNotesTypes()
    {
        return new int [0];
    } 

    //public string[] MidiData()
    //{
    //    MIDI = File.ReadAllBytes(midi);
    //    Debug.Log("MIDI Length : " + MIDI.Length.ToString());

    //    Debug.Log(((char)MIDI[0]).ToString() + ((char)MIDI[1]).ToString() + ((char)MIDI[2]).ToString() + ((char)MIDI[3]).ToString());
    //    Length = (MIDI[4] << 24) | (MIDI[5] << 16) | (MIDI[6] << 8) | MIDI[7];
    //    int format = (MIDI[8] << 8) | MIDI[9];
    //    int tracks = (MIDI[10] << 8) | MIDI[11];
    //    int division = (MIDI[12] << 8) | MIDI[13];

    //    Debug.Log("Length : " + Length.ToString());
    //    Debug.Log("Format : " + format.ToString());
    //    Debug.Log("Tracks : " + tracks.ToString());

    //    if ((division & (1 << 15)) == 0)
    //        //    Debug.Log("Delta Time : " + division.ToString());
    //        AbsoluteQuantize = division;
    //    else
    //           Debug.Log("Ticks/frame");

    //        Index = 14;

    //    if (PrintInfo)
    //    {
    //        for (int i = 0; i < MIDI.Length; i++)
    //        {
    //            //Debug.Log((i + 1) + " : " + MIDI[i]);
    //            if (Channels)
    //            {

    //                if (MIDI[i] >= 144 && MIDI[i] <= 159)
    //                {
    //                    if (OnlyChannelOne)
    //                    {
    //                        if (MIDI[i] != 144)
    //                            continue;
    //                    }

    //                    //if (NotesInHex)
    //                    //    Debug.Log("Channel " + (MIDI[i] - 143).ToString() + " On, Note " + MIDI[i + 1].ToString("X") + ", Velocity " + MIDI[i + 2]);
    //                    //else
    //                    //    Debug.Log("Channel " + (MIDI[i] - 143).ToString() + " On, Note " + MIDI[i + 1].ToString() + ", Velocity " + MIDI[i + 2]);
    //                    //continue;
    //                    Debug.Log("Note :" + MIDI[i + 1].ToString());

    //                    notes.Add(new MidiNote()
    //                    {
    //                        AbsoluteQuantize = AbsoluteQuantize,
    //                        Midi = MIDI[i + 1],
    //                        Chanel = (MIDI[i] - 143),
    //                        Velocity = MIDI[i + 2],
    //                        Duration = noteon.NoteLength * PulseLengthMs,
    //                        Patch = PatchChanel[trackEvent.Event.Channel - 1],
    //                        Drum = (trackEvent.Event.Channel == 10),
    //                        Delay = 0,
    //                    });
    //                }

    //                if (MIDI[i] >= 128 && MIDI[i] <= 143)
    //                {
    //                    if (OnlyChannelOne)
    //                    {
    //                        if (MIDI[i] != 128)
    //                            continue;
    //                    }
    //                    //if (NotesInHex)
    //                    //    Debug.Log("Channel " + (MIDI[i] - 127).ToString() + " Off, Note " + MIDI[i + 1].ToString("X") + ", Velocity " + MIDI[i + 2]);
    //                    //else
    //                    //    Debug.Log("Channel " + (MIDI[i] - 127).ToString() + " Off, Note " + MIDI[i + 1].ToString() + ", Velocity " + MIDI[i + 2]);
    //                    //continue;
    //                    Debug.Log("Note :" + MIDI[i + 1].ToString());
    //                }
    //            }
    //        }
    //    }
    //}


}
