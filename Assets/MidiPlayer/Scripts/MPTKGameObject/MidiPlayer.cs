
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
    /// Play a list of notes according the preset
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MidiPlayer : MonoBehaviour
    {
        /// <summary>
        /// Log information on selected wave for each notes
        /// </summary>
        public bool MPTK_LogWaves = false;

        /// <summary>
        /// Transpose note from -24 to 24
        /// </summary>
        public virtual int MPTK_Transpose
        {
            get { return transpose; }
            set
            {
                if (value >= -24 && value <= 24f)
                    transpose = value;
                else
                    Debug.LogWarning("MidiFilePlayer - Set Transpose value not valid : " + value);
            }
        }
        [SerializeField]
        [HideInInspector]
        public int transpose = 0;

        /// <summary>
        /// Volume of midi playing. 
        /// Must be >=0 and <= 1
        /// </summary>
        public virtual float MPTK_Volume
        {
            get { return volume; }
            set
            {
                if (volume >= 0f && volume <= 1f)
                    volume = value;
                else
                    Debug.LogWarning("MidiFilePlayer - Set Volume value not valid : " + value);
            }
        }

        [SerializeField]
        [HideInInspector]
        private float volume = 0.5f;

        /// <summary>
        /// Template for creating all AudioSource
        /// </summary>
        public AudioSource AudioSourceTemplate;

        static protected float _ratioHalfTone = 1.0594630943592952645618252949463f;
        protected List<AudioSource> audiosources;
        protected float VolumeTransitionTime = 0.1f;

        protected virtual void Awake()
        {
            try
            {
                HelperNoteLabel.Init();
                MidiPlayerGlobal.Init();
                audiosources = new List<AudioSource>();
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        protected virtual void Start()
        {
            try
            {
                AudioSourceTemplate = GetComponentInChildren<AudioSource>();
                if (AudioSourceTemplate == null)
                    Debug.LogWarning("No AudioSource found.");
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Clear all sounds
        /// </summary>
        public void ClearAllSound()
        {
            try
            {
                if (AudioSourceTemplate.isPlaying)
                    StartCoroutine(Release(AudioSourceTemplate, VolumeTransitionTime));
                foreach (AudioSource audio in audiosources)
                    if (audio.isPlaying)
                        StartCoroutine(Release(audio, VolumeTransitionTime));
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Remove AudioSource not playing
        /// </summary>
        protected void DestroyAllAudioSource()
        {
            try
            {
                for (int i = 0; i < audiosources.Count;)
                {
                    AudioSource audio = audiosources[i];
                    if (!audio.isPlaying)
                    {
                        //Debug.Log("delete audio source " +audio.transform.root.name);
                        try
                        {
                            audiosources.RemoveAt(i);
                            Destroy(audio.gameObject);
                        }
                        catch (System.Exception ex)
                        {
                            MidiPlayerGlobal.ErrorDetail(ex);
                        }
                    }
                    else
                        i++;
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Play a list of notes
        /// </summary>
        /// <param name="notes"></param>
        protected void PlayNotes(List<MidiNote> notes)
        {
            //if (MidiPlayerGlobal.SoundFontLoaded == false)
            //    return;

            if (notes != null)
            {
                foreach (MidiNote note in notes)
                {
                    try
                    {
                        // Search sample associated to the preset, midi note and velocity
                        int selectedBank = note.Drum ? MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo.DrumKitBankNumber : selectedBank = MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo.DefaultBankNumber;
                        int noteMidi = note.Midi;
                        if (note.Chanel != 10)
                            noteMidi += MPTK_Transpose;
                        ImSample smpl = MidiPlayerGlobal.GetImSample(selectedBank, note.Patch, noteMidi, note.Velocity);
                        if (smpl != null)
                        {
                            note.Pitch = Mathf.Pow(_ratioHalfTone, (float)(noteMidi - smpl.OriginalPitch + smpl.CoarseTune) + (float)smpl.FineTune / 100f);
                            // Load wave from audioclip
                            AudioClip clip = DicAudioClip.Get(smpl.WaveFile);
                            if (clip != null && clip.loadState == AudioDataLoadState.Loaded)
                            {
                                if (MPTK_LogWaves)
                                    LogInfoSample(note, smpl);

                                AudioSource audioSelected = null;

                                // Search audioclip not playing 
                                //try
                                //{
                                //    foreach (AudioSource audio in audiosources)
                                //    {
                                //        //Debug.Log(audio.isPlaying + " " + audio.clip.name + " " + clip.name);
                                //        if (!audio.isPlaying && audio.clip.name == clip.name)
                                //        {
                                //            audioSelected = audio;
                                //            break;
                                //        }
                                //    }
                                //}
                                //catch (System.Exception ex)
                                //{
                                //    MidiPlayerGlobal.ErrorDetail(ex);
                                //}
                                if (audioSelected == null)
                                {
                                    // No audiosource available, create a new audiosource
                                    audioSelected = Instantiate<AudioSource>(AudioSourceTemplate);
                                    audioSelected.Stop();
                                    audioSelected.transform.position = AudioSourceTemplate.transform.position;
                                    audioSelected.transform.SetParent(this.transform);
                                    audiosources.Add(audioSelected);
                                    // Assign sound to audioclip
                                    audioSelected.clip = clip;
                                }

                                // Play note
                                BroadcastMidiWave.inistance.Broadcast(audioSelected,note);
                                StartCoroutine(PlayNote(audioSelected, !note.Drum, note));

                            }
                            else
                            {
                                if (MPTK_LogWaves)
                                    LogInfoSample(note, null, smpl.WaveFile + "         ******** Clip not ready to play or not found ******");
                            }
                        }
                        else
                        {
                            if (MPTK_LogWaves)
                                LogInfoSample(note, null, "               ********* Sample not found *********");
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MidiPlayerGlobal.ErrorDetail(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Play one note with this AudioSource
        /// </summary>
        /// <param name="audio">AudioSource</param>
        /// <param name="loop">Sound with loop</param>
        /// <param name="note"></param>
        /// <returns></returns>
        protected IEnumerator PlayNote(AudioSource audio, bool loop, MidiNote note)
        {
            if (note.Delay > 0f)
            {
                float endDelay = Time.realtimeSinceStartup + note.Delay / 1000f;
                while (Time.realtimeSinceStartup < endDelay)
                    yield return new WaitForSeconds(0.01f);
            }
            try
            {
                audio.pitch = note.Pitch;
                audio.loop = loop;
                //audio.volume = note.Gain;
                audio.volume = Mathf.Lerp(0f, 1f, note.Velocity / 127f) * MPTK_Volume;
                audio.Play();
            }
            catch (Exception)
            {
            }

            // Attack & Decay are taken in account by the wave
            if (loop)
            {
                // Sustain phase until key release, constant amplitude
                float end = Time.realtimeSinceStartup + (float)(note.Duration / 1000d);
                while (true)
                {
                    try
                    {
                        if (Time.realtimeSinceStartup >= end || !audio.isPlaying)
                            break;
                    }
                    catch (Exception)
                    {
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                // Release phase
                if (VolumeTransitionTime > 0f)
                {
                    float dtime = 0f;
                    float volume = 0;

                    try
                    {
                        volume = audio.volume;
                        end = Time.realtimeSinceStartup + VolumeTransitionTime;

                    }
                    catch (Exception)
                    {
                    }

                    do
                    {
                        dtime = end - Time.realtimeSinceStartup;
                        try
                        {
                            audio.volume = Mathf.Lerp(0f, volume, dtime / VolumeTransitionTime);
                            if (dtime < 0f || !audio.isPlaying)
                                break;

                        }
                        catch (Exception)
                        {
                            break;
                        }
                        yield return new WaitForSeconds(0.01f);
                    }
                    while (true);

                    try
                    {
                        audio.Stop();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            //else
            //{
            //    // play with no loop (drum)
            //    while (audio.isPlaying)
            //    {
            //        yield return new WaitForSeconds(0.01f);
            //    }
            //}
        }

        /// <summary>
        /// Release the sound
        /// </summary>
        /// <param name="audio"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        protected IEnumerator Release(AudioSource audio, float delay)
        {
            float dtime = 0f;
            float volume = 0f;
            float end = 0f;
            if (delay > 0f)
            {
                try
                {
                    dtime = 0f;
                    volume = audio.volume;
                    end = Time.realtimeSinceStartup + delay;
                }
                catch (Exception)
                {
                }

                do
                {
                    dtime = end - Time.realtimeSinceStartup;
                    try
                    {
                        audio.volume = Mathf.Lerp(0f, volume, dtime / delay);
                        if (dtime < 0f || !audio.isPlaying)
                            break;
                    }
                    catch (Exception)
                    {
                        break;
                    }

                    yield return new WaitForSeconds(0.01f);
                }
                while (true);
            }

            try
            {
                audio.Stop();
            }
            catch (Exception)
            {
            }

        }

        private void LogInfoSample(MidiNote note, ImSample sample, string info = null)
        {
            //TimeSpan playtime = TimeSpan.Zero;
            //if (miditoplay != null) playtime = miditoplay.CurrentMidiTime();

            //string time = string.Format("[{0:00}:{1:00}:{2:00}:{3:000}]", playtime.Hours, playtime.Minutes, playtime.Seconds, playtime.Milliseconds);
            string time = "";
            if (sample != null)
#if DEBUGPITCHNOTE
                Debug.Log(string.Format("{0} C:{1,2} P:{2,3:000} D:{3} Note:{4,3:000} OriginalPitch:{5} PitchCorrection:{6} FineTune:{7} CoarseTune:{8} Duration:{9,4} sec. Velocity:{10} Wave:{11}",
                    time, note.Chanel, note.Patch, note.Drum, note.Midi,
                    sample.OriginalPitch, sample.PitchCorrection, sample.FineTune, sample.CoarseTune, Math.Round(note.Duration / 1000d, 2), note.Velocity, sample.WaveFile));
#else
            {
                Debug.Log(string.Format("{0} C:{1,2} P:{2,3:000} D:{3} Note:{4,3:000} {5,-3} Duration:{6,4} sec. Velocity:{7} Wave:{8}",
                    time, note.Chanel, note.Patch, note.Drum, note.Midi, HelperNoteLabel.LabelFromMidi(note.Midi),
                    Math.Round(note.Duration / 1000d, 2), note.Velocity, sample.WaveFile));
              //  Debug.Log("note: " + note.Midi);
                //var midiWave = new MidiWave
                //{
                //    Time = time,
                //    Chanel = note.Chanel,
                //    Patch = note.Patch,
                //    Drum = note.Drum,
                //    Midi = note.Midi,
                //    LabelFromMidi = HelperNoteLabel.LabelFromMidi(note.Midi),
                //    Duration = Math.Round(note.Duration / 1000d, 2),
                //    Velocity = note.Velocity,
                //    WaveFile = sample.WaveFile
                //};

            }
#endif
            else
                Debug.Log(string.Format("{0} C:{1,2} P:{2,3:000} D:{3} Note:{4,3:000} {5,-5} Duration:{6,4} sec. Velocity:{7} {8}",
                    time, note.Chanel, note.Patch, note.Drum, note.Midi, HelperNoteLabel.LabelFromMidi(note.Midi), Math.Round(note.Duration / 1000d, 2), note.Velocity, info));

        }
    }
}

//public struct MidiWave
//{
//    public string Time { get; internal set; }
//    public float Chanel { get; internal set; }
//    public int Patch { get; internal set; }
//    public bool Drum { get; internal set; }
//    public int Midi { get; internal set; }
//    public string LabelFromMidi { get; internal set; }
//    public double Duration { get; internal set; }
//    public int Velocity { get; internal set; }
//    public string WaveFile { get; internal set; }
//}