using MidiPlayerTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace MidiPlayerTK
{
    public class SoundFontInfo
    {
        public string Name;
        //public bool Active;
        public int DefaultBankNumber = -1;
        public int DrumKitBankNumber = -1;
        /// <summary>
        /// OS path of the imsf with the filename
        /// </summary>
        public string ImSFPath;
        /// <summary>
        /// resource path of the imsf with the filename
        /// </summary>
        public string ImSFResourcePath;
        /// <summary>
        /// Path to the original SF2 files with the filename
        /// </summary>
        public string SF2Path;
         
    }

    /// <summary>
    /// Contains the setting of MidiFilePlayer TK
    /// </summary>
    public class MidiSet
    {
        public List<SoundFontInfo> SoundFonts;
        public SoundFontInfo ActiveSounFontInfo;
        public List<string> MidiFiles;

        public MidiSet()
        {
            SoundFonts = new List<SoundFontInfo>();
        }

        public void SetActiveSoundFont(int index)
        {
            try
            {
                if (index > -1 && index < SoundFonts.Count)
                    ActiveSounFontInfo = SoundFonts[index];
                else
                    ActiveSounFontInfo = null;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Save setting (edit mode)
        /// </summary>
        public void Save()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(MidiSet));
                string path = Application.dataPath + "/" + MidiPlayerGlobal.PathToMidiSet;
                using (var stream = new FileStream(path, FileMode.Create))
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
        /// Load setting (edit mode)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MidiSet Load(string path)
        {
            MidiSet loaded = null;

            try
            {
                if (File.Exists(path))
                {
                    var serializer = new XmlSerializer(typeof(MidiSet));
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        loaded = serializer.Deserialize(stream) as MidiSet;
                    }
                }
                else
                    loaded = new MidiSet();
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }

            return loaded;
        }

        /// <summary>
        /// LOad setting (run mode)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MidiSet LoadRsc(string data)
        {
            MidiSet loaded = null;

            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    var serializer = new XmlSerializer(typeof(MidiSet));
                    using (TextReader reader = new StringReader(data))
                    {
                        loaded = serializer.Deserialize(reader) as MidiSet;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }

            return loaded;
        }
    }
}
