
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using UnityEngine.Events;

namespace MidiPlayerTK
{
    /// <summary>
    /// Script for the prefab MidiFilePlayer. 
    /// Play a selected midi file. 
    /// List of Midi file must be defined with Midi Player Setup (menu tools).
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MidiFilePlayer : MidiPlayer
    {
        /// <summary>
        /// Midi name to play. 
        /// Must exists in MidiPlayerGlobal.CurrentMidiSet.MidiFiles.
        /// List of Midi file must be defined with Midi Player Setup (menu tools)
        /// and contains that name.
        /// </summary>
        public virtual string MPTK_MidiName { get { return midiNameToPlay; } set { midiNameToPlay = value; } }
        [SerializeField]
        [HideInInspector]
        private string midiNameToPlay;

        /// <summary>
        /// Should the Midi start playing when application start ?
        /// </summary>
        public virtual bool MPTK_PlayOnStart { get { return playOnStart; } set { playOnStart = value; } }

        /// <summary>
        /// Should the Midi playing must be paused if distance between AudioListener and MidiFilePlayer is greater than MaxDistance
        /// </summary>
        public virtual bool MPTK_PauseOnDistance { get { return pauseOnDistance; } set { pauseOnDistance = value; } }

        /// <summary>
        /// MaxDistance to use for PauseOnDistance
        /// </summary>
        public virtual float MPTK_MaxDistance
        {
            get
            {
                try
                {
                    return AudioSourceTemplate.maxDistance;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
                return 0;
            }
            set
            {
                try
                {
                    AudioSourceTemplate.maxDistance = value;
                    if (audiosources == null) audiosources = new List<AudioSource>();
                    foreach (AudioSource audio in audiosources)
                        audio.maxDistance = value;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
            }
        }

        /// <summary>
        /// Value updated only when playing in Unity (for inspector refresh)
        /// </summary>
        public float distanceEditorModeOnly;

        /// <summary>
        /// Should automatically restart when Midi reach the end ?
        /// </summary>
        public virtual bool MPTK__Loop { get { return loop; } set { loop = value; } }

        /// <summary>
        /// Get default tempo defined in Midi file or modified with Speed. 
        /// Return QuarterPerMinuteValue similar to BPM (Beat Per Measure)
        /// </summary>
        public virtual double MPTK_Tempo { get { if (miditoplay != null) return miditoplay.QuarterPerMinuteValue; else return 0d; } }

        /// <summary>
        /// Should change tempo from Midi Events ? 
        /// </summary>
        public virtual bool MPTK_EnableChangeTempo
        {
            get
            {
                try
                {
                    if (miditoplay != null)
                        return miditoplay.EnableChangeTempo;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
                return false;
            }
            set
            {
                try
                {
                    if (miditoplay != null)
                        miditoplay.EnableChangeTempo = value;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
            }
        }
        /// <summary>
        /// Speed of playing. 
        /// Between 0.1 (10%) to 5.0 (500%). 
        /// Set to 1 for normal speed. 
        /// </summary>
        public virtual float MPTK_Speed
        {
            get { return speed; }
            set
            {
                try
                {
                    if (value >= 0.1f && value <= 5.0f)
                    {
                        MPTK_Pause(0.3f);
                        speed = value;
                        miditoplay.ChangeSpeed(speed);
                    }
                    else
                        Debug.LogWarning("MidiFilePlayer - Set Speed value not valid : " + value);
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
            }
        }

        /// <summary>
        /// Position to play from 0 ms to lenght time of midi playing (in millisecond)
        /// </summary>
        public virtual float MPTK_Position
        {
            get { return (float)timeFromStartMS; }
            set
            {
                try
                {
                    if (value >= 0f && value <= (float)MPTK_Duration.TotalMilliseconds)
                    {
                        MPTK_Pause(0.2f);
                        timeFromStartMS = value;
                        miditoplay.CalculateNextPosEvents(timeFromStartMS);
                    }
                    else
                        Debug.LogWarning("MidiFilePlayer - Set Position value not valid : " + value);
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
            }
        }

        [SerializeField]
        [HideInInspector]
        private float speed = 1f;

        /// <summary>
        /// Is Midi file playing is paused ?
        /// </summary>
        public virtual bool MPTK__IsPaused { get { return playPause; } }

        /// <summary>
        /// Is Midi file is playing ?
        /// </summary>
        public virtual bool MPTK_IsPlaying { get { return midiIsPlaying; } }

        /// <summary>
        /// Value updated only when playing in Unity (for inspector refresh)
        /// </summary>
        public string durationEditorModeOnly;

        /// <summary>
        /// Get duration of current Midi with current tempo
        /// </summary>
        public virtual TimeSpan MPTK_Duration { get { try { if (miditoplay != null) return miditoplay.Duration; } catch (System.Exception ex) { MidiPlayerGlobal.ErrorDetail(ex); } return TimeSpan.Zero; } }

        /// <summary>
        /// Lenght in millisecond of a quarter
        /// </summary>
        public virtual float MPTK_PulseLenght { get { try { if (miditoplay != null) return (float) miditoplay.PulseLengthMs; } catch (System.Exception ex) { MidiPlayerGlobal.ErrorDetail(ex); } return 0f; } }

        /// <summary>
        /// Updated only when playing in Unity (for inspector refresh)
        /// </summary>
        public string playTimeEditorModeOnly;

        /// <summary>
        /// Time from the start of playing the current midi
        /// </summary>
        public virtual TimeSpan MPTK_PlayTime { get { try { return TimeSpan.FromMilliseconds(timeFromStartMS); } catch (System.Exception ex) { MidiPlayerGlobal.ErrorDetail(ex); } return TimeSpan.Zero; } }

        /// <summary>
        /// Log midi events
        /// </summary>
        public virtual bool MPTK_LogEvents
        {
            get
            {
                try
                {
                    if (miditoplay != null)
                        return miditoplay.LogEvents;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
                return false;
            }
            set
            {
                try
                {
                    if (miditoplay != null)
                        miditoplay.LogEvents = value;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
            }
        }

        /// <summary>
        /// Define unity event to trigger at start
        /// </summary>
        [HideInInspector]
        public UnityEvent OnEventStartPlayMidi;

        /// <summary>
        /// Define unity event to trigger at end
        /// </summary>
        [HideInInspector]
        public UnityEvent OnEventEndPlayMidi;

        /// <summary>
        /// Level of quantization : 
        ///     0 = none to 
        ///     5 = 64th Note
        /// </summary>
        public virtual int MPTK_Quantization
        {
            get { return quantization; }
            set
            {
                try
                {
                    if (value >= 0 && value <= 5)
                    {
                        quantization = value;
                        miditoplay.ChangeQuantization(quantization);
                    }
                    else
                        Debug.LogWarning("MidiFilePlayer - Set Quantization value not valid : " + value);
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
            }
        }
        [SerializeField]
        [HideInInspector]
        private int quantization = 0;


        [SerializeField]
        [HideInInspector]
        private bool playOnStart = false, pauseOnDistance = false, newMidiToPlay = false, stopMidiToPlay = false, midiIsPlaying = false, loop = false, playPause = false;

        private float delayMilliSeconde = 10f;
        private float timeToPauseMilliSeconde = -1f;
        public double timeFromStartMS = 0d;
        private MidiLoad miditoplay;

        /// <summary>
        /// Index Midi to play or playing. 
        /// return -1 if not found
        /// </summary>
        /// <param name="index"></param>
        public virtual int MPTK_MidiIndex
        {
            get
            {
                try
                {
                    return MidiPlayerGlobal.MPTK_FindMidi(MPTK_MidiName);
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
                return -1;
            }
            set
            {
                try
                {
                    if (value >= 0 && value < MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count)
                        MPTK_MidiName = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[value];
                    else
                        Debug.LogWarning("MidiFilePlayer - Set MidiIndex value not valid : " + value);
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }
            }
        }

        new void Awake()
        {
            base.Awake();
        }

        new void Start()
        {
            base.Start();
            try
            {
                if (MPTK_PlayOnStart)
                {
                    StartCoroutine(TheadPlayIfReady());
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        private IEnumerator TheadPlayIfReady()
        {
            while (!MidiPlayerGlobal.SoundFontLoaded)
                yield return new WaitForSeconds(0.2f);

            // Wait a few of millisecond to let app to start (usefull when play on start)
            yield return new WaitForSeconds(0.2f);

            MPTK_Play();
        }

        /// <summary>
        /// Play the midi file
        /// </summary>
        public virtual void MPTK_Play()
        {
            try
            {
                if (MidiPlayerGlobal.SoundFontLoaded)
                {
                    playPause = false;
                    if (!midiIsPlaying)
                    {
                        // Load description of available soundfont
                        if (MidiPlayerGlobal.ImSFCurrent != null && MidiPlayerGlobal.CurrentMidiSet != null && MidiPlayerGlobal.CurrentMidiSet.MidiFiles != null && MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count > 0)
                        {
                            if (string.IsNullOrEmpty(MPTK_MidiName))
                                MPTK_MidiName = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[0];
                            int selectedMidi = MidiPlayerGlobal.CurrentMidiSet.MidiFiles.FindIndex(s => s == MPTK_MidiName);
                            if (selectedMidi >= 0)
                            {
                                ClearAllSound();
                                StartCoroutine(TheadPlay());
                                //StartCoroutine(TestWithDelay());
                            }
                            else
                                Debug.LogWarning("MidiFilePlayer - midi file not found in the inspector list " + MPTK_MidiName);
                        }
                        else
                            Debug.LogWarning("MidiFilePlayer - no sound font or midi set defined");
                    }
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Stop playing
        /// </summary>
        public virtual void MPTK_Stop()
        {
            try
            {
                playPause = false;
                stopMidiToPlay = true;
                ClearAllSound();
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Restart playing the current midi file
        /// </summary>
        public virtual void MPTK_RePlay()
        {
            try
            {
                playPause = false;
                if (midiIsPlaying)
                {
                    ClearAllSound();
                    newMidiToPlay = true;
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Pause the current playing
        /// </summary>
        public virtual void MPTK_Pause(float timeToPauseMS = -1f)
        {
            try
            {
                timeToPauseMilliSeconde = timeToPauseMS;
                playPause = true;
                ClearAllSound();
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Play next Midi in list
        /// </summary>
        public virtual void MPTK_Next()
        {
            try
            {
                if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles != null && MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count > 0)
                {
                    int selectedMidi = 0;
                    if (!string.IsNullOrEmpty(MPTK_MidiName))
                        selectedMidi = MidiPlayerGlobal.CurrentMidiSet.MidiFiles.FindIndex(s => s == MPTK_MidiName);
                    if (selectedMidi >= 0)
                    {
                        selectedMidi++;
                        if (selectedMidi >= MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count)
                            selectedMidi = 0;
                        MPTK_MidiName = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[selectedMidi];
                        MPTK_RePlay();
                    }
                }
                else
                    Debug.LogWarning("MidiFilePlayer - no Midi defined, go to menu 'Tools/Midi Player Toolkit Setup'");
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Play previous Midi in list
        /// </summary>
        public virtual void MPTK_Previous()
        {
            try
            {
                if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles != null && MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count > 0)
                {
                    int selectedMidi = 0;
                    if (!string.IsNullOrEmpty(MPTK_MidiName))
                        selectedMidi = MidiPlayerGlobal.CurrentMidiSet.MidiFiles.FindIndex(s => s == MPTK_MidiName);
                    if (selectedMidi >= 0)
                    {
                        selectedMidi--;
                        if (selectedMidi < 0)
                            selectedMidi = MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count - 1;
                        MPTK_MidiName = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[selectedMidi];
                        MPTK_RePlay();
                    }
                }
                else
                    Debug.LogWarning("MidiFilePlayer - no Midi defined, go to menu 'Tools/Midi Player Toolkit Setup'");
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        private IEnumerator TheadPlay()
        {
            midiIsPlaying = true;
            stopMidiToPlay = false;
            newMidiToPlay = false;
            //Debug.Log("Start play");
            try
            {
                TextAsset mididata = Resources.Load<TextAsset>(Path.Combine(MidiPlayerGlobal.MidiFilesDB, MPTK_MidiName));
                miditoplay = new MidiLoad(mididata.bytes);
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }

            if (miditoplay != null)
            {

                float lastTimePlay = 0;
                try
                {
                    OnEventStartPlayMidi.Invoke();
                    DestroyAllAudioSource();
                    lastTimePlay = Time.realtimeSinceStartup;
                    timeFromStartMS = 0f;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }

                try
                {
                    miditoplay.ChangeSpeed(MPTK_Speed);
                    miditoplay.CancelNextReadEvents = false;
                    miditoplay.ChangeQuantization(MPTK_Quantization);
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }

                // Loop on each events midi
                do
                {
                    if (MPTK_PauseOnDistance)
                    {
                        distanceEditorModeOnly = MidiPlayerGlobal.MPTK_DistanceToListener(this.transform);
                        if (distanceEditorModeOnly > AudioSourceTemplate.maxDistance)
                        {
                            lastTimePlay = Time.realtimeSinceStartup;
                            yield return new WaitForSeconds(0.2f);
                            continue;
                        }
                    }

                    if (playPause)
                    {
                        lastTimePlay = Time.realtimeSinceStartup;
                        yield return new WaitForSeconds(0.2f);
                        if (timeToPauseMilliSeconde > -1f)
                        {
                            timeToPauseMilliSeconde -= 0.2f;
                            if (timeToPauseMilliSeconde <= 0f)
                                playPause = false;
                        }
                        continue;
                    }

                    timeFromStartMS += (Time.realtimeSinceStartup - lastTimePlay) * 1000f;
                    lastTimePlay = Time.realtimeSinceStartup;

                    // Play
                    List<MidiNote> notes = miditoplay.ReadMidiEvents(timeFromStartMS);

                    if (miditoplay.EndMidiEvent || newMidiToPlay || stopMidiToPlay)
                    {
                        //yield return new WaitForSeconds(0.5f);
                        break;
                    }

                    if (notes != null && notes.Count > 0)
                    {
                        PlayNotes(notes);
                        //Debug.Log("---------------- play count:" + notes.Count + " " + timeFromStartMS );
                    }
                    if (Application.isEditor)
                    {
                        TimeSpan times = TimeSpan.FromMilliseconds(timeFromStartMS);
                        playTimeEditorModeOnly = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", times.Hours, times.Minutes, times.Seconds, times.Milliseconds);
                        durationEditorModeOnly = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", MPTK_Duration.Hours, MPTK_Duration.Minutes, MPTK_Duration.Seconds, MPTK_Duration.Milliseconds);
                    }

                    yield return new WaitForSeconds(delayMilliSeconde / 1000f);// 0.01f);
                }
                while (true);
            }

            midiIsPlaying = false;

            try
            {
                if (OnEventEndPlayMidi != null && !stopMidiToPlay)
                    OnEventEndPlayMidi.Invoke();

                if (MPTK__Loop && !stopMidiToPlay)
                    MPTK_Play();
                stopMidiToPlay = false;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            //Debug.Log("Stop play");

        }

        /// <summary>
        /// For unitary test
        /// </summary>
        /// <returns></returns>
        IEnumerator TestWithDelay()
        {
            int velocity = 62;
            int startmidi = 66;
            int endmidi = 68;
            //int startmidi = 30;
            //int endmidi = 108;
            int duration = 500;
            do
            {
                List<MidiNote> notes = new List<MidiNote>();
                for (int note = startmidi; note <= endmidi; note++)
                    notes.Add(new MidiNote() { Midi = note, Velocity = velocity, Duration = duration, Delay = (note - startmidi) * duration });

                PlayNotes(notes);
                yield return new WaitForSeconds((endmidi - startmidi) * duration / 1000f + 2f);
                Debug.Log("End loop");
            } while (MPTK__Loop && !stopMidiToPlay);
        }

        //private string InfoNote(MidiNote note)
        //{
        //    return string.Format("Time:{0,8} Pitch:{1:0.00000000} Duration:{2:00000} Delay:{3:000000}", Time.realtimeSinceStartup, note.Pitch, note.Duration, note.Delay);
        //}


    }
}

