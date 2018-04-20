using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MidiPlayerTK
{
    public class HelperNoteLabel
    {

        public int Midi;
        public string Label;
        //public float Ratio;
        //public float Frequence;

        static List<HelperNoteLabel> ListNote;
        static List<HelperNoteLabel> ListEcart;
        static public float _ratioHalfTone = 0.0594630943592952645618252949463f;

        //static public float RatioFromHauteur(int hauteur)
        //{
        //    int index = hauteur + 39;
        //    if (index < 0 || index >= ListNote.Count)
        //        return 1f;
        //    else
        //        return ListNote[index].Ratio;
        //}

        static public string LabelFromMidi(int midi)
        {
            try
            {
                if (midi < 0 || midi >= ListNote.Count)
                    return "xx";
                else
                    return ListNote[midi].Label;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return "xx";
        }

        static public string LabelFromEcart(int ecart)
        {
            try
            {
                if (ecart < 0 || ecart >= 12)
                    return "xx";
                else
                    return ListEcart[ecart].Label;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return "xx";
        }
        static public void Init()
        {
            try
            {
                ListEcart = new List<HelperNoteLabel>();
                ListEcart.Add(new HelperNoteLabel() { Label = "C", Midi = 0, });
                ListEcart.Add(new HelperNoteLabel() { Label = "C#", Midi = 1, });
                ListEcart.Add(new HelperNoteLabel() { Label = "D", Midi = 2, });
                ListEcart.Add(new HelperNoteLabel() { Label = "D#", Midi = 3, });
                ListEcart.Add(new HelperNoteLabel() { Label = "E", Midi = 4, });
                ListEcart.Add(new HelperNoteLabel() { Label = "F", Midi = 5, });
                ListEcart.Add(new HelperNoteLabel() { Label = "F#", Midi = 6, });
                ListEcart.Add(new HelperNoteLabel() { Label = "G", Midi = 7, });
                ListEcart.Add(new HelperNoteLabel() { Label = "G#", Midi = 8, });
                ListEcart.Add(new HelperNoteLabel() { Label = "A", Midi = 9, });
                ListEcart.Add(new HelperNoteLabel() { Label = "A#", Midi = 10, });
                ListEcart.Add(new HelperNoteLabel() { Label = "B", Midi = 11, });

                ListNote = new List<HelperNoteLabel>();
                ListNote.Add(new HelperNoteLabel() { Label = "C0", Midi = 0, });
                ListNote.Add(new HelperNoteLabel() { Label = "C0#", Midi = 1, });
                ListNote.Add(new HelperNoteLabel() { Label = "D0", Midi = 2, });
                ListNote.Add(new HelperNoteLabel() { Label = "D0#", Midi = 3, });
                ListNote.Add(new HelperNoteLabel() { Label = "E0", Midi = 4, });
                ListNote.Add(new HelperNoteLabel() { Label = "F0", Midi = 5, });
                ListNote.Add(new HelperNoteLabel() { Label = "F0#", Midi = 6, });
                ListNote.Add(new HelperNoteLabel() { Label = "G0", Midi = 7, });
                ListNote.Add(new HelperNoteLabel() { Label = "G0#", Midi = 8, });
                ListNote.Add(new HelperNoteLabel() { Label = "A0", Midi = 9, });
                ListNote.Add(new HelperNoteLabel() { Label = "A0#", Midi = 10, });
                ListNote.Add(new HelperNoteLabel() { Label = "B0", Midi = 11, });
                ListNote.Add(new HelperNoteLabel() { Label = "C1", Midi = 12, });
                ListNote.Add(new HelperNoteLabel() { Label = "C1#", Midi = 13, });
                ListNote.Add(new HelperNoteLabel() { Label = "D1", Midi = 14, });
                ListNote.Add(new HelperNoteLabel() { Label = "D1#", Midi = 15, });
                ListNote.Add(new HelperNoteLabel() { Label = "E1", Midi = 16, });
                ListNote.Add(new HelperNoteLabel() { Label = "F1", Midi = 17, });
                ListNote.Add(new HelperNoteLabel() { Label = "F1#", Midi = 18, });
                ListNote.Add(new HelperNoteLabel() { Label = "G1", Midi = 19, });
                ListNote.Add(new HelperNoteLabel() { Label = "G1#", Midi = 20, });
                ListNote.Add(new HelperNoteLabel() { Label = "A1", Midi = 21, });
                ListNote.Add(new HelperNoteLabel() { Label = "A1#", Midi = 22, });
                ListNote.Add(new HelperNoteLabel() { Label = "B1", Midi = 23, });
                ListNote.Add(new HelperNoteLabel() { Label = "C2", Midi = 24, });
                ListNote.Add(new HelperNoteLabel() { Label = "C2#", Midi = 25, });
                ListNote.Add(new HelperNoteLabel() { Label = "D2", Midi = 26, });
                ListNote.Add(new HelperNoteLabel() { Label = "D2#", Midi = 27, });
                ListNote.Add(new HelperNoteLabel() { Label = "E2", Midi = 28, });
                ListNote.Add(new HelperNoteLabel() { Label = "F2", Midi = 29, });
                ListNote.Add(new HelperNoteLabel() { Label = "F2#", Midi = 30, });
                ListNote.Add(new HelperNoteLabel() { Label = "G2", Midi = 31, });
                ListNote.Add(new HelperNoteLabel() { Label = "G2#", Midi = 32, });
                ListNote.Add(new HelperNoteLabel() { Label = "A2", Midi = 33, });
                ListNote.Add(new HelperNoteLabel() { Label = "A2#", Midi = 34, });
                ListNote.Add(new HelperNoteLabel() { Label = "B2", Midi = 35, });
                ListNote.Add(new HelperNoteLabel() { Label = "C3", Midi = 36, });
                ListNote.Add(new HelperNoteLabel() { Label = "C3#", Midi = 37, });
                ListNote.Add(new HelperNoteLabel() { Label = "D3", Midi = 38, });
                ListNote.Add(new HelperNoteLabel() { Label = "D3#", Midi = 39, });
                ListNote.Add(new HelperNoteLabel() { Label = "E3", Midi = 40, });
                ListNote.Add(new HelperNoteLabel() { Label = "F3", Midi = 41, });
                ListNote.Add(new HelperNoteLabel() { Label = "F3#", Midi = 42, });
                ListNote.Add(new HelperNoteLabel() { Label = "G3", Midi = 43, });
                ListNote.Add(new HelperNoteLabel() { Label = "G3#", Midi = 44, });
                ListNote.Add(new HelperNoteLabel() { Label = "A3", Midi = 45, });
                ListNote.Add(new HelperNoteLabel() { Label = "A3#", Midi = 46, });
                ListNote.Add(new HelperNoteLabel() { Label = "B3", Midi = 47, });
                ListNote.Add(new HelperNoteLabel() { Label = "C4", Midi = 48, });
                ListNote.Add(new HelperNoteLabel() { Label = "C4#", Midi = 49, });
                ListNote.Add(new HelperNoteLabel() { Label = "D4", Midi = 50, });
                ListNote.Add(new HelperNoteLabel() { Label = "D4#", Midi = 51, });
                ListNote.Add(new HelperNoteLabel() { Label = "E4", Midi = 52, });
                ListNote.Add(new HelperNoteLabel() { Label = "F4", Midi = 53, });
                ListNote.Add(new HelperNoteLabel() { Label = "F4#", Midi = 54, });
                ListNote.Add(new HelperNoteLabel() { Label = "G4", Midi = 55, });
                ListNote.Add(new HelperNoteLabel() { Label = "G4#", Midi = 56, });
                ListNote.Add(new HelperNoteLabel() { Label = "A4", Midi = 57, });
                ListNote.Add(new HelperNoteLabel() { Label = "A4#", Midi = 58, });
                ListNote.Add(new HelperNoteLabel() { Label = "B4", Midi = 59, });
                ListNote.Add(new HelperNoteLabel() { Label = "C5", Midi = 60, });
                ListNote.Add(new HelperNoteLabel() { Label = "C5#", Midi = 61, });
                ListNote.Add(new HelperNoteLabel() { Label = "D5", Midi = 62, });
                ListNote.Add(new HelperNoteLabel() { Label = "D5#", Midi = 63, });
                ListNote.Add(new HelperNoteLabel() { Label = "E5", Midi = 64, });
                ListNote.Add(new HelperNoteLabel() { Label = "F5", Midi = 65, });
                ListNote.Add(new HelperNoteLabel() { Label = "F5#", Midi = 66, });
                ListNote.Add(new HelperNoteLabel() { Label = "G5", Midi = 67, });
                ListNote.Add(new HelperNoteLabel() { Label = "G5#", Midi = 68, });
                ListNote.Add(new HelperNoteLabel() { Label = "A5", Midi = 69, });
                ListNote.Add(new HelperNoteLabel() { Label = "A5#", Midi = 70, });
                ListNote.Add(new HelperNoteLabel() { Label = "B5", Midi = 71, });
                ListNote.Add(new HelperNoteLabel() { Label = "C6", Midi = 72, });
                ListNote.Add(new HelperNoteLabel() { Label = "C6#", Midi = 73, });
                ListNote.Add(new HelperNoteLabel() { Label = "D6", Midi = 74, });
                ListNote.Add(new HelperNoteLabel() { Label = "D6#", Midi = 75, });
                ListNote.Add(new HelperNoteLabel() { Label = "E6", Midi = 76, });
                ListNote.Add(new HelperNoteLabel() { Label = "F6", Midi = 77, });
                ListNote.Add(new HelperNoteLabel() { Label = "F6#", Midi = 78, });
                ListNote.Add(new HelperNoteLabel() { Label = "G6", Midi = 79, });
                ListNote.Add(new HelperNoteLabel() { Label = "G6#", Midi = 80, });
                ListNote.Add(new HelperNoteLabel() { Label = "A6", Midi = 81, });
                ListNote.Add(new HelperNoteLabel() { Label = "A6#", Midi = 82, });
                ListNote.Add(new HelperNoteLabel() { Label = "B6", Midi = 83, });
                ListNote.Add(new HelperNoteLabel() { Label = "C7", Midi = 84, });
                ListNote.Add(new HelperNoteLabel() { Label = "C7#", Midi = 85, });
                ListNote.Add(new HelperNoteLabel() { Label = "D7", Midi = 86, });
                ListNote.Add(new HelperNoteLabel() { Label = "D7#", Midi = 87, });
                ListNote.Add(new HelperNoteLabel() { Label = "E7", Midi = 88, });
                ListNote.Add(new HelperNoteLabel() { Label = "F7", Midi = 89, });
                ListNote.Add(new HelperNoteLabel() { Label = "F7#", Midi = 90, });
                ListNote.Add(new HelperNoteLabel() { Label = "G7", Midi = 91, });
                ListNote.Add(new HelperNoteLabel() { Label = "G7#", Midi = 92, });
                ListNote.Add(new HelperNoteLabel() { Label = "A7", Midi = 93, });
                ListNote.Add(new HelperNoteLabel() { Label = "A7#", Midi = 94, });
                ListNote.Add(new HelperNoteLabel() { Label = "B7", Midi = 95, });
                ListNote.Add(new HelperNoteLabel() { Label = "C8", Midi = 96, });
                ListNote.Add(new HelperNoteLabel() { Label = "C8#", Midi = 97, });
                ListNote.Add(new HelperNoteLabel() { Label = "D8", Midi = 98, });
                ListNote.Add(new HelperNoteLabel() { Label = "D8#", Midi = 99, });
                ListNote.Add(new HelperNoteLabel() { Label = "E8", Midi = 100, });
                ListNote.Add(new HelperNoteLabel() { Label = "F8", Midi = 101, });
                ListNote.Add(new HelperNoteLabel() { Label = "F8#", Midi = 102, });
                ListNote.Add(new HelperNoteLabel() { Label = "G8", Midi = 103, });
                ListNote.Add(new HelperNoteLabel() { Label = "G8#", Midi = 104, });
                ListNote.Add(new HelperNoteLabel() { Label = "A8", Midi = 105, });
                ListNote.Add(new HelperNoteLabel() { Label = "A8#", Midi = 106, });
                ListNote.Add(new HelperNoteLabel() { Label = "B8", Midi = 107, });
                ListNote.Add(new HelperNoteLabel() { Label = "C9", Midi = 108, });
                ListNote.Add(new HelperNoteLabel() { Label = "C9#", Midi = 109, });
                ListNote.Add(new HelperNoteLabel() { Label = "D9", Midi = 110, });
                ListNote.Add(new HelperNoteLabel() { Label = "D9#", Midi = 111, });
                ListNote.Add(new HelperNoteLabel() { Label = "E9", Midi = 112, });
                ListNote.Add(new HelperNoteLabel() { Label = "F9", Midi = 113, });
                ListNote.Add(new HelperNoteLabel() { Label = "F9#", Midi = 114, });
                ListNote.Add(new HelperNoteLabel() { Label = "G9", Midi = 115, });
                ListNote.Add(new HelperNoteLabel() { Label = "G9#", Midi = 116, });
                ListNote.Add(new HelperNoteLabel() { Label = "A9", Midi = 117, });
                ListNote.Add(new HelperNoteLabel() { Label = "A9#", Midi = 118, });
                ListNote.Add(new HelperNoteLabel() { Label = "B9", Midi = 119, });
                ListNote.Add(new HelperNoteLabel() { Label = "C10", Midi = 120, });
                ListNote.Add(new HelperNoteLabel() { Label = "C10#", Midi = 121, });
                ListNote.Add(new HelperNoteLabel() { Label = "D10", Midi = 122, });
                ListNote.Add(new HelperNoteLabel() { Label = "D10#", Midi = 123, });
                ListNote.Add(new HelperNoteLabel() { Label = "E10", Midi = 124, });
                ListNote.Add(new HelperNoteLabel() { Label = "F10", Midi = 125, });
                ListNote.Add(new HelperNoteLabel() { Label = "F10#", Midi = 126, });
                ListNote.Add(new HelperNoteLabel() { Label = "G10", Midi = 127, });
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            // For test
            //ListNote[60].Ratio = 1f; // C3
            //ListNote[60].Frequence = 261.626f; // C3

            //foreach (HelperNote hn in ListNote)
            //{
            //    hn.Ratio = Mathf.Pow(_ratioHalfTone, hn.Midi);
            //    hn.Frequence = ListNote[48].Frequence * hn.Ratio;
            //    //Debug.Log("Position:" + hn.Position +" Hauteur:" + hn.Hauteur +" Label:" + hn.Label +" Ratio:" + hn.Ratio +" Frequence:" + hn.Frequence);
            //}
        }
    }

    public class HelperNoteRatio
    {
        public int Delta;
        public float Ratio;
        static float[] Ratios;
        static public float _ratioHalfTone = 1.0594630943592952645618252949463f;
        static public float _rationCents = 1.0005777895f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta">from -60 to 60</param>
        /// <returns></returns>
        static public float Get(int delta,int finetune)
        {
            try
            {
                return Mathf.Pow(_ratioHalfTone, (float)delta + (float)finetune / 100f);
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return 0f;
        }
        static public void Init()
        {
            try
            {
                Ratios = new float[120];
                for (int index = 0; index < 120; index++)
                {
                    Ratios[index] = Mathf.Pow(_ratioHalfTone, index - 60);
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        } 
    }
}
