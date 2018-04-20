using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using NAudio.Midi;
using System;
using System.IO;

namespace MidiPlayerTK
{
    /// <summary>
    /// Scan a midifile and return information
    /// </summary>
    public class MidiScan
    {
        /// <summary>
        /// Return information about a midifile : patch change, copyright, ...
        /// </summary>
        /// <param name="pathfilename"></param>
        /// <param name="Info"></param>
        static public void GeneralInfo(string pathfilename, BuilderInfo Info)
        {
            try
            {
                int NumberBeatsMeasure;
                int NumberQuarterBeat;
                Debug.Log("Open midifile :" + pathfilename);
                MidiLoad midifile = new MidiLoad(pathfilename);

                if (midifile != null)
                {
                    Info.Add(string.Format("Format: {0}", midifile.midifile.FileFormat));
                    Info.Add(string.Format("Tracks: {0}", midifile.midifile.Tracks));
                    Info.Add(string.Format("Ticks Quarter Note: {0}", midifile.midifile.DeltaTicksPerQuarterNote));

                    foreach (TrackMidiEvent trackEvent in midifile.MidiSorted)
                    {
                        if (trackEvent.Event.CommandCode == MidiCommandCode.NoteOn)
                        {
                            // Not used 
                            //if (((NoteOnEvent)trackEvent.Event).OffEvent != null)
                            //{
                            //    //infoTrackMidi[e.Channel].Events.Add((NoteOnEvent)e);
                            //    NoteOnEvent noteon = (NoteOnEvent)trackEvent.Event;
                            //}
                        }
                        else if (trackEvent.Event.CommandCode == MidiCommandCode.NoteOff)
                        {
                        }
                        else if (trackEvent.Event.CommandCode == MidiCommandCode.ControlChange)
                        {
                            // Not used 
                            //ControlChangeEvent controlchange = (ControlChangeEvent)e;
                            //Debug.Log(string.Format("CtrlChange  Track:{0} Channel:{1,2:00} {2}", track, e.Channel, controlchange.ToString()));
                        }
                        else if (trackEvent.Event.CommandCode == MidiCommandCode.PatchChange)
                        {
                            PatchChangeEvent change = (PatchChangeEvent)trackEvent.Event;
                            Info.Add(BuildInfoTrack(trackEvent) + string.Format("PatchChange {0,3:000} {1}", change.Patch, PatchChangeEvent.GetPatchName(change.Patch)),2);
                        }
                        else if (trackEvent.Event.CommandCode == MidiCommandCode.MetaEvent)
                        {
                            MetaEvent meta = (MetaEvent)trackEvent.Event;
                            switch (meta.MetaEventType)
                            {
                                case MetaEventType.SetTempo:
                                    TempoEvent tempo = (TempoEvent)meta;
                                    Info.Add(BuildInfoTrack(trackEvent) + string.Format("SetTempo Tempo:{0} MicrosecondsPerQuarterNote:{1}", Math.Round(tempo.Tempo, 0), tempo.MicrosecondsPerQuarterNote),2);
                                    //tempo.Tempo
                                    break;
                                case MetaEventType.TimeSignature:

                                    TimeSignatureEvent timesig = (TimeSignatureEvent)meta;
                                    // Numerator: counts the number of beats in a measure. 
                                    // For example a numerator of 4 means that each bar contains four beats. 

                                    // Denominator: number of quarter notes in a beat.0=ronde, 1=blanche, 2=quarter, 3=eighth, etc. 
                                    // Set default value
                                    NumberBeatsMeasure = timesig.Numerator;
                                    NumberQuarterBeat = System.Convert.ToInt32(System.Math.Pow(2, timesig.Denominator));
                                    Info.Add(BuildInfoTrack(trackEvent) + string.Format("Beats Measure:{0} Beat Quarter:{1}", NumberBeatsMeasure, NumberQuarterBeat),2);
                                    break;
                                case MetaEventType.SequenceTrackName: // Sequence / Track Name
                                case MetaEventType.ProgramName:
                                case MetaEventType.TrackInstrumentName: // Track instrument name
                                case MetaEventType.TextEvent: // Text event
                                case MetaEventType.Copyright: // Copyright
                                    Info.Add(BuildInfoTrack(trackEvent) + ((TextEvent)meta).Text,1);
                                    break;
                                case MetaEventType.Lyric: // lyric
                                case MetaEventType.Marker: // marker
                                case MetaEventType.CuePoint: // cue point
                                case MetaEventType.DeviceName:
                                    // Info.Add(BuildInfoTrack(trackEvent) + string.Format("{0} '{1}'", meta.MetaEventType.ToString(), ((TextEvent)meta).Text));
                                    break;
                            }
                        }
                        else
                        {
                            // Other midi event
                            //Debug.Log(string.Format("Track:{0} Channel:{1,2:00} CommandCode:{2,3:000} AbsoluteTime:{3,6:000000}", track, e.Channel, e.CommandCode.ToString(), e.AbsoluteTime));
                        }
                    }
                }
                else
                {
                    Info.Add("Error reading midi file");
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

       private static string BuildInfoTrack(TrackMidiEvent e)
        {
            return string.Format("[{0,5:00000}] [T:{1,2:00} C:{2,2:00}] ", e.Event.AbsoluteTime, e.IndexTrack, e.Event.Channel);
        }
    }
}

