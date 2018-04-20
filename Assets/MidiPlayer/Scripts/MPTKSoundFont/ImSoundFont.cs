using MidiPlayerTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace MidiPlayerTK
{
    /// <summary>
    /// SoundFont adapted to Unity
    /// </summary>
    public class ImSoundFont
    {
        public string SoundFontName;
        public int PatchCount;
        public int WaveCount;
        public long WaveSize;
        public int DefaultBankNumber = -1;
        public int DrumKitBankNumber = -1;
        public bool KeepAllPatchs = false;
        public bool KeepAllZones = false;
        public const int MAXBANKPRESET = 129;
        /// <summary>
        /// List  of banks of the sound font
        /// </summary>
        public ImBank[] Banks;

        public int FirstBank()
        {
            int ibank = 0;
            try
            {
                while (Banks[ibank] == null && ibank < Banks.Length) ibank++;
                if (ibank == Banks.Length) ibank = 0;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return ibank;
        }
        public int LastBank()
        {
            int ibank = Banks.Length - 1;
            try
            {
                while (Banks[ibank] == null && ibank >= 0) ibank--;
                if (ibank < 0) ibank = 0;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return ibank;
        }
        /// <summary>
        /// Save an ImSoundFont 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public void Save(string path, string name)
        {
            try
            {
                string Filepath = Path.Combine(path, name);
                var serializer = new XmlSerializer(typeof(ImSoundFont));

                using (var stream = new FileStream(Filepath + MidiPlayerGlobal.ExtensionSoundFileFile, FileMode.Create))
                {
                    serializer.Serialize(stream, this);
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }
        /// <summary>
        /// Load an ImSoundFont from a string
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ImSoundFont Load(string data)
        {
            ImSoundFont loaded = null;

            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    var serializer = new XmlSerializer(typeof(ImSoundFont));
                    using (TextReader reader = new StringReader(data))
                    {
                        loaded = serializer.Deserialize(reader) as ImSoundFont;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return loaded;
        }
        /// <summary>
        /// Load an ImSoundFont from a desktop file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ImSoundFont Load(string path, string name)
        {
            ImSoundFont loaded = null;
            try
            {
                string Filepath = Path.Combine(path, name);
                Filepath += MidiPlayerGlobal.ExtensionSoundFileFile;

                if (File.Exists(Filepath))
                {
                    var serializer = new XmlSerializer(typeof(ImSoundFont));
                    using (var stream = new FileStream(Filepath, FileMode.Open))
                    {
                        loaded = serializer.Deserialize(stream) as ImSoundFont;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
            return loaded;
        }
        /// <summary>
        /// Add infoormation about preset
        /// </summary>
        public void CreateBankDescription()
        {
            try
            {
                foreach (ImBank bank in Banks)
                    if (bank != null)
                    {
                        bank.Description = "";
                        foreach (ImPreset preset in bank.Presets)
                            if (preset != null)
                                bank.Description += preset.Name + " ; ";
                    }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }
    }

    /// <summary>
    /// ImBank of an ImSoundFont
    /// </summary>
    public class ImBank
    {
        public int BankNumber;
        public ImPreset[] Presets;
        [XmlIgnore]
        public string Description;
    }

    /// <summary>
    /// Preset from a ImSoundFont
    /// </summary>
    public class ImPreset
    {
        public string Name;
        public int Bank;
        public int Patch;
        public int Key;
        public List<ImInstrument> Instruments;
        public string Description()
        {
            return string.Format(" {0,3:000} {1}", Patch, Name);
        }
    }

    /// <summary>
    /// Instrument from a ImSoundFont
    /// </summary>
    public class ImInstrument
    {
        public int KeyStart;
        public int KeyEnd;
        public bool HasKey;
        public int VelStart;
        public int VelEnd;
        public bool HasVel;
        public int Pan;
        public int RootKey;
        public List<ImSample> Samples;
        public string SampleName;
        public bool HasSample;
    }

    /// <summary>
    /// Sample from a ImSoundFont
    /// </summary>
    public class ImSample
    {
        public int KeyStart;
        public int KeyEnd;
        public bool HasKey;
        public int VelStart;
        public int VelEnd;
        public bool HasVel;
        public int Pan;

        /// <summary>
        /// Contains the MIDI key number of the recorded pitch of the sample. 
        /// For example, a recording of an instrument playing middle C(261.62 Hz) should receive a value of 60. 
        /// This value is used as the default “root key” for the sample, so that in the example, a MIDI key-on command for note number 60 would reproduce the sound at its original pitch.
        /// For unpitched sounds, a conventional value of 255 should be used.Values between 128 and 254 are illegal.
        /// Whenever an illegal value or a value of 255 is encountered, the value 60 should be used.
        /// </summary>
        public int OriginalPitch;

        /// <summary>
        /// This parameter represents the MIDI key number at which the sample is to be played back at its original sample rate.
        /// If not present, or if present with a value of -1, then the sample header parameter Original Key is used in its place. 
        /// If it is present in the range 0-127, then the indicated key number will cause the sample to be played back at its sample header Sample Rate.
        /// For example, if the sample were a recording of a piano middle C (Original Key = 60) at a sample rate of 22.050 kHz, 
        /// and Root Key were set to 69, then playing MIDI key number 69 (A above middle C) would cause a piano note of pitch middle C to be heard.
        /// </summary>
        //public int OverridingRootKey;

        /// <summary>
        /// This is a pitch offset, in semitones, which should be applied to the note. 
        /// A positive value indicates the sound is reproduced at a higher pitch; a negative value indicates a lower pitch.
        /// For example, a Coarse Tune value of -4 would cause the sound to be reproduced four semitones flat.
        /// </summary>
        public int CoarseTune;

        /// <summary>
        /// This is a pitch offset, in cents, which should be applied to the note.
        /// It is additive with coarseTune.
        /// A positive value indicates the sound is reproduced at a higher pitch; 
        /// A negative value indicates a lower pitch.For example, a Fine Tuning value of -5 would cause the sound to be reproduced five cents flat.
        /// </summary>
        public int FineTune;

        /// <summary>
        /// Contains a pitch correction in cents that should be applied to the sample on playback. 
        /// The purpose of this field is to compensate for any pitch errors during the sample recording process.
        /// The correction value is that of the correction to be applied.
        /// For example, if the sound is 4 cents sharp, a correction bringing it 4 cents flat is required; thus the value should be -4. 
        /// </summary>
        public int PitchCorrection;

        //public string Name;
        public string WaveFile;
    }
}
