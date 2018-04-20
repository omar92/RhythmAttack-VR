using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using NAudio.Midi;
using System;
using System.IO;
using System.Linq;

namespace MidiPlayerTK
{
    public class TrackMidiEvent
    {
        public int IndexTrack;
        public long AbsoluteQuantize;
        public MidiEvent Event;
    }

    public class MidiLoad
    {
        public MidiFile midifile;
        public List<TrackMidiEvent> MidiSorted;
        public bool CancelNextReadEvents = false;
        /// <summary>
        /// Duration of the midi. Updated when ChangeSpeed is called.
        /// </summary>
        public TimeSpan Duration;
        public bool EndMidiEvent;
        public double QuarterPerMinuteValue;
        public bool EnableChangeTempo;
        public bool LogEvents;

        public double PulseLengthMs;
        private double PulseInSecond;
        private long Quantization;
        private long CurrentPulse = 0;
        private double Speed = 1d;
        private double LastTimeFromStartMS;

        /// <summary>
        /// Last position played by tracks
        /// </summary>
        private int NextPosEvent;

        /// <summary>
        /// Last patch change by chanel
        /// </summary>
        private int[] PatchChanel;

        /// <summary>
        /// Build OS path to the midi file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public string BuildOSPath(string filename)
        {
            try
            {
                string pathMidiFolder = Path.Combine(Application.dataPath, MidiPlayerGlobal.PathToMidiFile);
                string pathfilename = Path.Combine(pathMidiFolder, filename + MidiPlayerGlobal.ExtensionMidiFile);
                return pathfilename;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return null;
        }

        /// <summary>
        /// Load Midi from an array of bytes
        /// </summary>
        /// <param name="datamidi"></param>
        public MidiLoad(byte[] datamidi)
        {
            try
            {
                midifile = new MidiFile(datamidi, false);
                if (midifile != null)
                    Init();
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Load Midi from a Midi file (OS)
        /// </summary>
        /// <param name="filename"></param>
        public MidiLoad(string filename)
        {
            string pathfilename = BuildOSPath(filename);
            try
            {
                midifile = new MidiFile(pathfilename, false);
                if (midifile != null)
                    Init();
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        private void Init()
        {
            try
            {
                CurrentPulse = 0;
                NextPosEvent = 0;
                PatchChanel = new int[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                QuarterPerMinuteValue = double.NegativeInfinity;
                MidiSorted = GetEvents();
                if (QuarterPerMinuteValue < 0d)
                    ChangeTempo(80d);
                //Debug.Log("DeltaTicksPerQuarterNote:" + midifile.DeltaTicksPerQuarterNote);
                //Debug.Log("Pulse par minute:" + QuarterPerMinuteValue * midifile.DeltaTicksPerQuarterNote);
                PulseInSecond = (QuarterPerMinuteValue * midifile.DeltaTicksPerQuarterNote) / 60f;
                //Debug.Log("Pulse par seconde:" + PulseInSecond);
                PulseLengthMs = 1000d / PulseInSecond;
                //Debug.Log("Pulse length in milli seconde:" + PulseLengthMs);
                //The BPM measures how many quarter notes happen in a minute. To work out the length of each pulse we can use the following formula: Pulse Length = 60 / (BPM * PPQN)
                //16  Sixteen Double croche
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        private List<TrackMidiEvent> GetEvents()
        {
            try
            {
                int iTrack = 0;
                List<TrackMidiEvent> events = new List<TrackMidiEvent>();
                foreach (IList<MidiEvent> track in midifile.Events)
                {
                    iTrack++;
                    foreach (MidiEvent e in track)
                    {
                        try
                        {
                            bool keepEvent = false;
                            switch (e.CommandCode)
                            {
                                case MidiCommandCode.NoteOn:
                                    if (((NoteOnEvent)e).OffEvent != null)
                                        keepEvent = true;
                                    break;
                                case MidiCommandCode.PatchChange:
                                    keepEvent = true;
                                    break;
                                case MidiCommandCode.MetaEvent:
                                    MetaEvent meta = (MetaEvent)e;
                                    switch (meta.MetaEventType)
                                    {
                                        case MetaEventType.SetTempo:
                                            // Set the first tempo value find
                                            if (QuarterPerMinuteValue < 0d)
                                                ChangeTempo(((TempoEvent)e).Tempo);
                                            keepEvent = true;
                                            break;
                                        case MetaEventType.SequenceTrackName:
                                        case MetaEventType.ProgramName:
                                        case MetaEventType.TrackInstrumentName:
                                        case MetaEventType.TextEvent:
                                        case MetaEventType.Copyright:
                                            keepEvent = true;
                                            break;
                                    }
                                    break;
                            }
                            if (keepEvent)
                                events.Add(new TrackMidiEvent() { IndexTrack = iTrack, Event = e.Clone() });
                        }
                        catch (System.Exception ex)
                        {
                            MidiPlayerGlobal.ErrorDetail(ex);
                        }
                    }
                }

                List<TrackMidiEvent> MidiSorted = events.OrderBy(o => o.Event.AbsoluteTime).ToList();
                return MidiSorted;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return null;
        }
        /// <summary>
        /// Change speed to play. 1=normal speed
        /// </summary>
        /// <param name="speed"></param>
        public void ChangeSpeed(float speed)
        {
            try
            {
                Speed = speed;
                if (QuarterPerMinuteValue > 0d)
                {
                    ChangeTempo(QuarterPerMinuteValue);
                    //CancelNextReadEvents = true;
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        public void ChangeQuantization(int level = 4)
        {
            try
            {
                if (level <= 0)
                    Quantization = 0;
                else
                    Quantization = midifile.DeltaTicksPerQuarterNote / level;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Calculate PulseLenghtMS from QuarterPerMinute value
        /// </summary>
        /// <param name="tempo"></param>
        private void ChangeTempo(double tempo)
        {
            try
            {
                QuarterPerMinuteValue = tempo;
                PulseLengthMs = (1000d / ((QuarterPerMinuteValue * midifile.DeltaTicksPerQuarterNote) / 60f)) / Speed;
                // UPdate total time of midi play
                if (MidiSorted != null && MidiSorted.Count > 0)
                    Duration = TimeSpan.FromMilliseconds(MidiSorted[MidiSorted.Count - 1].Event.AbsoluteTime * PulseLengthMs);
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Get current time of playing
        /// </summary>
        /// <returns></returns>
        public TimeSpan CurrentMidiTime()
        {
            return TimeSpan.FromMilliseconds(CurrentPulse * PulseLengthMs);
        }

        public void CalculateNextPosEvents(double timeFromStartMS)
        {
            if (MidiSorted != null)
            {
                CurrentPulse = Convert.ToInt64(timeFromStartMS / PulseLengthMs);

                //Debug.Log(">>> CalculateNextPosEvents - CurrentPulse:" + CurrentPulse + " CurrentNextPosEvent:" + NextPosEvent + " LastTimeFromStartMS:" + LastTimeFromStartMS + " timeFromStartMS:" + Convert.ToInt32(timeFromStartMS));
                LastTimeFromStartMS = timeFromStartMS;
                // From the last position played
                for (int currentPosEvent = 0; currentPosEvent < MidiSorted.Count; currentPosEvent++)
                {
                    TrackMidiEvent trackEvent = MidiSorted[currentPosEvent];
                    //if (currentPosEvent + 1 < MidiSorted.Count)
                    {
                        //TrackMidiEvent nexttrackEvent = MidiSorted[currentPosEvent + 1];
                        //Debug.Log("CurrentPulse:" + CurrentPulse+ " trackEvent:" + trackEvent.AbsoluteQuantize+ " nexttrackEvent:" + nexttrackEvent.AbsoluteQuantize);

                        if (trackEvent.Event.AbsoluteTime > CurrentPulse)// && CurrentPulse < nexttrackEvent.Event.AbsoluteTime )
                        {
                            NextPosEvent = currentPosEvent;
                            //Debug.Log("     CalculateNextPosEvents - NextPosEvent:" + NextPosEvent + " trackEvent:" + trackEvent.Event.AbsoluteTime + " timeFromStartMS:" + Convert.ToInt32(timeFromStartMS));
                            //Debug.Log("NextPosEvent:" + NextPosEvent);
                            break;
                        }
                        //if (currentPosEvent == MidiSorted.Count - 1) Debug.Log("Last CalculateNextPosEvents - currentPosEvent:" + currentPosEvent + " trackEvent:" + trackEvent.Event.AbsoluteTime + " timeFromStartMS:" + Convert.ToInt32(timeFromStartMS));
                    }
                }
                //Debug.Log("<<< CalculateNextPosEvents");
            }
        }
        public List<MidiNote> ReadMidiEvents(double timeFromStartMS)
        {
            List<MidiNote> notes = null;
            try
            {
                EndMidiEvent = false;
                if (midifile != null)
                {
                    if (NextPosEvent < MidiSorted.Count)
                    {
                        // The BPM measures how many quarter notes happen in a minute. To work out the length of each pulse we can use the following formula: 
                        // Pulse Length = 60 / (BPM * PPQN)
                        // Calculate current pulse to play
                        CurrentPulse += Convert.ToInt64((timeFromStartMS - LastTimeFromStartMS) / PulseLengthMs);

                        LastTimeFromStartMS = timeFromStartMS;

                        // From the last position played
                        for (int currentPosEvent = NextPosEvent; currentPosEvent < MidiSorted.Count; currentPosEvent++)
                        {
                            TrackMidiEvent trackEvent = MidiSorted[currentPosEvent];
                            if (Quantization != 0)
                                trackEvent.AbsoluteQuantize = ((trackEvent.Event.AbsoluteTime + Quantization / 2) / Quantization) * Quantization;
                            else
                                trackEvent.AbsoluteQuantize = trackEvent.Event.AbsoluteTime;

                            //    Debug.Log("ReadMidiEvents - timeFromStartMS:" + Convert.ToInt32(timeFromStartMS) + " LastTimeFromStartMS:" + Convert.ToInt32(LastTimeFromStartMS) + " CurrentPulse:" + CurrentPulse + " AbsoluteQuantize:" + trackEvent.AbsoluteQuantize);

                            if (trackEvent.AbsoluteQuantize <= CurrentPulse)
                            {
                                NextPosEvent = currentPosEvent + 1;

                                if (trackEvent.Event.CommandCode == MidiCommandCode.NoteOn)
                                {
                                    if (((NoteOnEvent)trackEvent.Event).OffEvent != null)
                                    {
                                        //infoTrackMidi[e.Channel].Events.Add((NoteOnEvent)e);
                                        NoteOnEvent noteon = (NoteOnEvent)trackEvent.Event;
                                        if (noteon.OffEvent != null)
                                        {
                                            if (LogEvents)
                                                Debug.Log(BuildInfoTrack(trackEvent) + string.Format("NoteOn  Note:{0,3:000} Lenght:{1} Veloc:{2}", noteon.NoteNumber, noteon.NoteLength, noteon.Velocity));
                                            if (notes == null) notes = new List<MidiNote>();

                                            //Debug.Log(string.Format("Track:{0} NoteNumber:{1,3:000} AbsoluteTime:{2,6:000000} NoteLength:{3,6:000000} OffDeltaTime:{4,6:000000} ", track, noteon.NoteNumber, noteon.AbsoluteTime, noteon.NoteLength, noteon.OffEvent.DeltaTime));
                                            notes.Add(new MidiNote()
                                            {
                                                AbsoluteQuantize = trackEvent.AbsoluteQuantize,
                                                Midi = noteon.NoteNumber,
                                                Chanel = trackEvent.Event.Channel,
                                                Velocity = noteon.Velocity,
                                                Duration = noteon.NoteLength * PulseLengthMs,
                                                Patch = PatchChanel[trackEvent.Event.Channel - 1],
                                                Drum = (trackEvent.Event.Channel == 10),
                                                Delay = 0,
                                            });
                                        }
                                    }
                                }
                                else if (trackEvent.Event.CommandCode == MidiCommandCode.NoteOff)
                                {
                                }
                                else if (trackEvent.Event.CommandCode == MidiCommandCode.ControlChange)
                                {
                                    // Not used
                                    ControlChangeEvent controlchange = (ControlChangeEvent)trackEvent.Event;
                                    // Other midi event
                                    if (LogEvents)
                                        Debug.Log(BuildInfoTrack(trackEvent) + string.Format("Control {0} {1}", controlchange.Controller, controlchange.ControllerValue));

                                }
                                else if (trackEvent.Event.CommandCode == MidiCommandCode.PatchChange)
                                {
                                    PatchChangeEvent change = (PatchChangeEvent)trackEvent.Event;
                                    PatchChanel[trackEvent.Event.Channel - 1] = trackEvent.Event.Channel == 10 ? 0 : change.Patch;
                                    if (LogEvents)
                                        Debug.Log(BuildInfoTrack(trackEvent) + string.Format("Patch   {0,3:000} {1}", change.Patch, PatchChangeEvent.GetPatchName(change.Patch)));
                                }
                                else if (trackEvent.Event.CommandCode == MidiCommandCode.MetaEvent)
                                {
                                    MetaEvent meta = (MetaEvent)trackEvent.Event;
                                    switch (meta.MetaEventType)
                                    {
                                        case MetaEventType.SetTempo:
                                            if (EnableChangeTempo)
                                            {
                                                TempoEvent tempo = (TempoEvent)meta;
                                                //NewQuarterPerMinuteValue = tempo.Tempo;
                                                ChangeTempo(tempo.Tempo);
                                                if (LogEvents)
                                                    Debug.Log(BuildInfoTrack(trackEvent) + string.Format("SetTempo   {0} MicrosecondsPerQuarterNote:{1}", tempo.Tempo, tempo.MicrosecondsPerQuarterNote));
                                            }
                                            break;
                                        //case MetaEventType.TextEvent: // Text event
                                        case MetaEventType.Copyright: // Copyright
                                        case MetaEventType.SequenceTrackName: // Sequence / Track Name
                                        case MetaEventType.TrackInstrumentName: // Track instrument name
                                        case MetaEventType.Lyric: // lyric
                                        case MetaEventType.Marker: // marker
                                        case MetaEventType.CuePoint: // cue point
                                        case MetaEventType.ProgramName:
                                        case MetaEventType.DeviceName:
                                            break;
                                    }
                                }
                                else
                                {
                                    // Other midi event
                                    //Debug.Log(string.Format("Track:{0} Channel:{1,2:00} CommandCode:{2,3:000} AbsoluteTime:{3,6:000000}", track, e.Channel, e.CommandCode.ToString(), e.AbsoluteTime));
                                }
                            }
                            else
                                // Out of time, exit for loop
                                break;
                        }

                        if (notes != null)
                        {
                            //if (CancelNextReadEvents)
                            //{
                            //    notes = null;
                            //    //Debug.Log("CancelNextReadEvents");
                            //    CancelNextReadEvents = false;
                            //}
                            //else
                            //if (notes.Count > 3 && (notes[notes.Count - 1].AbsoluteQuantize - notes[0].AbsoluteQuantize) > midifile.DeltaTicksPerQuarterNote * 8)
                            //{
                            //    //notes.RemoveRange(0, notes.Count - 1);
                            //    Debug.Log("--> Too much notes " + notes.Count + " timeFromStartMS:" + Convert.ToInt32(timeFromStartMS) + " Start:" + notes[0].AbsoluteQuantize + " Ecart:" + (notes[notes.Count - 1].AbsoluteQuantize - notes[0].AbsoluteQuantize) + " CurrentPulse:" + CurrentPulse);
                            //    //notes = null;
                            //}
                        }
                    }
                    else
                    {
                        // End of midi events
                        EndMidiEvent = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return notes;
        }

        private string BuildInfoTrack(TrackMidiEvent e)
        {
            return string.Format("[A:{0,5:00000} Q:{1,5:00000} P:{2,5:00000}] [T:{3,2:00} C:{4,2:00}] ", e.Event.AbsoluteTime, e.AbsoluteQuantize, CurrentPulse, e.IndexTrack, e.Event.Channel);
        }

        private void DebugTrack()
        {
            int itrck = 0;
            foreach (IList<MidiEvent> track in midifile.Events)
            {
                itrck++;
                foreach (MidiEvent midievent in track)
                {
                    string info = string.Format("Track:{0} Channel:{1,2:00} Command:{2} AbsoluteTime:{3:0000000} ", itrck, midievent.Channel, midievent.CommandCode, midievent.AbsoluteTime);
                    if (midievent.CommandCode == MidiCommandCode.NoteOn)
                    {
                        NoteOnEvent noteon = (NoteOnEvent)midievent;
                        if (noteon.OffEvent == null)
                            info += string.Format(" OffEvent null");
                        else
                            info += string.Format(" OffEvent.DeltaTimeChannel:{0:0000.00} ", noteon.OffEvent.DeltaTime);
                    }
                    Debug.Log(info);
                }
            }
        }
    }
}

