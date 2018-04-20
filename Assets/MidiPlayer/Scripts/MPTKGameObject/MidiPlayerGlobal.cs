using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;
using System;
using System.Collections.ObjectModel;

namespace MidiPlayerTK
{
    public class MidiPlayerGlobal
    {
        // public static MidiPlayerGlobal instance;
        public const string SoundfontsDB = "SoundfontsDB";
        public const string MidiFilesDB = "MidiDB";
        public const string PathToResources = "MidiPlayer/Resources/";
        public const string PathToSoundfonts = PathToResources + SoundfontsDB;
        public const string ExtensionMidiFile = ".bytes";
        public const string ExtensionSoundFileFile = ".txt";
        public const string PathToMidiFile = PathToResources + MidiFilesDB;
        public const string FilenameMidiSet = "MidiSet";
        public const string PathToMidiSet = PathToResources + FilenameMidiSet + ".txt";
        public const string PathSF2 = "SoundFont";
        public const string PathToWave = "wave";

        /// <summary>
        /// True if soundfont is loaded
        /// </summary>
        public static bool SoundFontLoaded = false;
        private static TimeSpan timeToLoadSoundFont = TimeSpan.MaxValue;
        public static TimeSpan MPTK_TimeToLoadSoundFont
        {
            get
            {
                return timeToLoadSoundFont;
            }
        }

        private static TimeSpan timeToLoadWave = TimeSpan.MaxValue;
        public static TimeSpan MPTK_TimeToLoadWave
        {
            get
            {
                return timeToLoadWave;
            }
        }

        public static int MPTK_CountPresetLoaded;
        public static int MPTK_CountWaveLoaded;

        /// <summary>
        /// Current Simplified SoundFont loaded
        /// </summary>
        public static ImSoundFont ImSFCurrent;

        /// <summary>
        /// Event triggered when Soundfont is loaded
        /// </summary>
        public static UnityEvent OnEventPresetLoaded = new UnityEvent();

        /// <summary>
        /// Current Midi Set loaded
        /// </summary>
        public static MidiSet CurrentMidiSet;

        private static string WavePath;
        private static AudioListener AudioListener;
        private static bool Initialized = false;

        /// <summary>
        /// List of midi(s) available
        /// </summary>
        public static ReadOnlyCollection<string> MPTK_ListMidis
        {
            get
            {
                if (CurrentMidiSet != null && CurrentMidiSet.MidiFiles!=null)
                    return CurrentMidiSet.MidiFiles.AsReadOnly();
                else
                    return null;
            }
        }

     

        /// <summary>
        /// Call by the first MidiPlayer awake
        /// </summary>
        public static void Init()
        {
            if (!Initialized)
            {
                SoundFontLoaded = false;
                Initialized = true;

                try
                {
                    AudioListener = Component.FindObjectOfType<AudioListener>();
                    if (AudioListener == null)
                    {
                        Debug.LogWarning("No audio listener found. Add one and only one AudioListener component to your hierarchy.");
                        //return;
                    }
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }

                try
                {
                    AudioListener[] listeners = Component.FindObjectsOfType<AudioListener>();
                    if (listeners != null && listeners.Length > 1)
                    {
                        Debug.LogWarning("More than one audio listener found. Some unexpected behaviors could happen.");
                    }
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }

                try
                {
                    LoadMidiSetFromRsc();
                    DicAudioClip.Init();
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }

                if (CurrentMidiSet == null)
                {
                    Debug.LogWarning("No Midi set found. Create a midi set from the menu Tools/Midi Player Toolkit");
                    return;
                }

                if (CurrentMidiSet.ActiveSounFontInfo == null)
                {
                    Debug.LogWarning("No Active SoundFont found. Define Sound Font from the menu Tools/Midi Player Toolkit");
                    return;
                }

                // Load simplfied soundfont
                try
                {
                    DateTime start = DateTime.Now;
                    SoundFontInfo sfi = CurrentMidiSet.ActiveSounFontInfo;
                    string pathToImSF = Path.Combine(sfi.ImSFResourcePath, sfi.Name);

                    TextAsset sf = Resources.Load<TextAsset>(pathToImSF);
                    if (sf == null)
                        Debug.LogError("No Simplified SoundFont found " + pathToImSF);
                    else
                    {
                        WavePath = Path.Combine(sfi.ImSFResourcePath, PathToWave);
                        // Load all presets defined in the XML sf
                        ImSFCurrent = ImSoundFont.Load(sf.text);
                        timeToLoadSoundFont = DateTime.Now - start;
                    }
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }

                if (ImSFCurrent == null)
                {
                    Debug.LogWarning("Simplified SoundFont not loaded.");
                    return;
                }

                // Load samples
                try
                {
                    MPTK_CountWaveLoaded = 0;
                    MPTK_CountPresetLoaded = 0;
                    DateTime start = DateTime.Now;
                    for (int ibank = 0; ibank < ImSFCurrent.Banks.Length; ibank++)
                    {
                        if (ImSFCurrent.Banks[ibank] != null)
                        {
                            for (int ipreset = 0; ipreset < ImSFCurrent.Banks[ibank].Presets.Length; ipreset++)
                            {
                                MPTK_CountPresetLoaded++;
                                if (ImSFCurrent.Banks[ibank].Presets[ipreset] != null)
                                {
                                    LoadSamples(ibank, ipreset);
                                }
                            }
                        }
                    }
                    timeToLoadWave = DateTime.Now - start;
                }
                catch (System.Exception ex)
                {
                    MidiPlayerGlobal.ErrorDetail(ex);
                }

                if (OnEventPresetLoaded != null) OnEventPresetLoaded.Invoke();
                SoundFontLoaded = true;
            }
        }

        /// <summary>
        /// Find index of a Midi file which contains "name". 
        /// return -1 if not found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int MPTK_FindMidi(string name)
        {
            int index = -1;
            try
            {
                if (!string.IsNullOrEmpty(name))
                    if (CurrentMidiSet != null && CurrentMidiSet.MidiFiles != null)
                        index = CurrentMidiSet.MidiFiles.FindIndex(s => s.Contains(name));

            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return index;
        }

        /// <summary>
        /// Calculate distance with the AudioListener
        /// </summary>
        /// <param name="trf"></param>
        /// <returns></returns>
        public static float MPTK_DistanceToListener(Transform trf)
        {
            float distance = 0f;
            try
            {
                if (AudioListener != null)
                {
                    distance = Vector3.Distance(AudioListener.transform.position, trf.position);
                    //Debug.Log("Camera:" + AudioListener.name + " " + distance);
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }

            return distance;
        }


        /// <summary>
        /// Load samples associated to a patch
        /// </summary>
        /// <param name="ibank"></param>
        /// <param name="ipatch"></param>
        private static void LoadSamples(int ibank, int ipatch)
        {
            try
            {
                float start = Time.realtimeSinceStartup;
                //Debug.Log(">>> Load Preset - b:" + ibank + " p:" + ipatch);
                if (ImSFCurrent != null)
                {

                    ImPreset preset = ImSFCurrent.Banks[ibank].Presets[ipatch];
                    //Debug.Log("Loading Preset - " + index + " '" + preset.Name + "'");
                    // Load each sample associated with this preset in DicAudioClip
                    foreach (ImInstrument inst in preset.Instruments)
                    {
                        foreach (ImSample smpl in inst.Samples)
                        {
                            if (smpl.WaveFile != null)
                            {
                                if (!DicAudioClip.Exist(smpl.WaveFile))
                                {
                                    AudioClip ac = Resources.Load<AudioClip>(WavePath + "/" + Path.GetFileNameWithoutExtension(smpl.WaveFile));
                                    if (ac != null)
                                    {
                                        DicAudioClip.Add(smpl.WaveFile, ac);
                                        MPTK_CountWaveLoaded++;
                                    }
                                    //else Debug.LogWarning("Wave " + smpl.WaveFile + " not found");
                                }
                            }
                        }
                    }
                    //Debug.Log("--- Loaded Preset - " + ipatch + " '" + preset.Name + "' " + count + " samples loaded");
                }
                else Debug.Log("Presets not loaded ");
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Load setup MPTK from resource
        /// </summary>
        private static void LoadMidiSetFromRsc()
        {
            try
            {
                TextAsset sf = Resources.Load<TextAsset>(MidiPlayerGlobal.FilenameMidiSet);
                if (sf == null)
                    Debug.LogWarning("No Midi set found. Create a midi set from the menu Tools/Midi Player Toolkit");
                else
                {
                    //UnityEngine.Debug.Log(sf.text);
                    CurrentMidiSet = MidiSet.LoadRsc(sf.text);
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Return sample to play according preset, key and velocity
        /// </summary>
        /// <param name="idxbank"></param>
        /// <param name="idxpreset"></param>
        /// <param name="key"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        public static ImSample GetImSample(int idxbank, int idxpreset, int key, int vel)
        {
            try
            {
                ImPreset preset = ImSFCurrent.Banks[idxbank].Presets[idxpreset];
                if (preset != null && preset.Instruments != null)
                {
                    foreach (ImInstrument instrument in preset.Instruments)
                    {
                        // if (inst.HasSample)
                        {
                            if (!instrument.HasKey || (key >= instrument.KeyStart && key <= instrument.KeyEnd))
                            {
                                if (!instrument.HasVel || (vel >= instrument.VelStart && vel <= instrument.VelEnd))
                                {
                                    foreach (ImSample sample in instrument.Samples)
                                    {
                                        if (sample.WaveFile != null)
                                        {
                                            if (!sample.HasKey || (key >= sample.KeyStart && key <= sample.KeyEnd))
                                            {
                                                if (!sample.HasVel || (vel >= sample.VelStart && vel <= sample.VelEnd))
                                                {
                                                    return sample;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return null;
        }

        public static void ErrorDetail(System.Exception ex)
        {
            Debug.LogWarning("MPTK Error " + ex.Message);
            Debug.LogWarning("   " + ex.TargetSite ?? "");
            Debug.LogWarning("   " + ex.StackTrace ?? "");
        }
    }
}
