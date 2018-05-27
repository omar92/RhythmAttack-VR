using MidiPlayerTK;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MidiPlayerInitialiser
{


    public static void Init(string[] tracks, string tracksResurcesFolder)
    {
        //clean old data
        try
        {
            if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count > 0)
            {
                for (int i = 0; i < MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count; i++)
                {
                    var fileName = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[i];
                    DeleteResource(MidiLoad.BuildOSPath(MidiPlayerGlobal.CurrentMidiSet.MidiFiles[i]));
                    //Debug.Log(MidiPlayerGlobal.CurrentMidiSet.MidiFiles);
                }
            }
        }
        catch (System.Exception)
        {

        }

        //load tracks files 
        for (int i = 0; i < tracks.Length; i++)
        {
            DirectoryInfo dir = new DirectoryInfo(tracksResurcesFolder+tracks[i]);
            FileInfo[] FilesInfo = dir.GetFiles("*.mid");
            foreach (FileInfo track in FilesInfo)
            {
                //add to tracks list
                AddMidifile(track.FullName);
            }
        }




        // Load description of available soundfont
        try
        {
            LoadMidiSet();
            CheckMidiSet();
            AssetDatabase.Refresh();
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    public static string[] GetTracksLocations(string directory)
    {
        DirectoryInfo dir = new DirectoryInfo(directory);
        FileInfo[] FilesInfo = dir.GetFiles("*.mid");

        string[] dirs = new string[FilesInfo.Length];
        for (int i = 0; i < FilesInfo.Length; i++)
        {
            dirs[i] = FilesInfo[i].FullName;
        }
        return dirs;
    }


    static void DeleteResource(string filepath)
    {
        try
        {
            //Debug.Log("Delete " + filepath);
            File.Delete(filepath);
            // delete also meta
            string meta = filepath + ".meta";
            //Debug.Log("Delete " + meta);
            File.Delete(meta);

        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    public static void LoadMidiSet()
    {
        try
        {
            MidiPlayerGlobal.CurrentMidiSet = MidiSet.Load(Path.Combine(Application.dataPath, MidiPlayerGlobal.PathToMidiSet));
            if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles == null)
                MidiPlayerGlobal.CurrentMidiSet.MidiFiles = new List<string>();
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    public static void AddMidifile(string selectedFile)
    {
        try
        {
            if (!string.IsNullOrEmpty(selectedFile))
            {
                //  MidiPlayerToolsEdit.lastDirectoryMidi = Path.GetDirectoryName(selectedFile);

                // Build path to midi folder 
                string pathMidiFile = Path.Combine(Application.dataPath, MidiPlayerGlobal.PathToMidiFile);
                if (!Directory.Exists(pathMidiFile))
                    Directory.CreateDirectory(pathMidiFile);

                string filenameToSave = Path.Combine(pathMidiFile, Path.GetFileNameWithoutExtension(selectedFile) + MidiPlayerGlobal.ExtensionMidiFile);
                // Create a copy of the midi file in resources
                File.Copy(selectedFile, filenameToSave, true);

                if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles == null)
                    MidiPlayerGlobal.CurrentMidiSet.MidiFiles = new List<string>();

                MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Add(Path.GetFileNameWithoutExtension(selectedFile));
                MidiPlayerGlobal.CurrentMidiSet.Save();
            }
            //    AssetDatabase.Refresh();
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    /// <summary>
    /// Update list SoundFont and Midi
    /// </summary>
    public static void CheckMidiSet()
    {
        // Activate one time to renum all the midi files in resource folder
        RenumMidiFile();

        //
        // Check Soundfont
        //
        try
        {
            string folder = Path.Combine(Application.dataPath, MidiPlayerGlobal.PathToSoundfonts);
            if (Directory.Exists(folder))
            {
                bool tobesaved = false;
                string[] fileEntries = Directory.GetFiles(folder, "*" + MidiPlayerGlobal.ExtensionSoundFileFile, SearchOption.AllDirectories);

                // Check if sf has been removed by user in resource
                int isf = 0;
                while (isf < MidiPlayerGlobal.CurrentMidiSet.SoundFonts.Count)
                {
                    bool found = false;
                    foreach (string filepath in fileEntries)
                        if (filepath.Contains(MidiPlayerGlobal.CurrentMidiSet.SoundFonts[isf].Name))
                            found = true;
                    if (!found)
                    {
                        MidiPlayerGlobal.CurrentMidiSet.SoundFonts.RemoveAt(isf);
                        tobesaved = true;
                    }
                    else
                        isf++;
                }

                // Active sound font exists in midiset ?
                if (MidiPlayerGlobal.CurrentMidiSet != null && MidiPlayerGlobal.ImSFCurrent != null)
                {
                    if (MidiPlayerGlobal.CurrentMidiSet.SoundFonts != null && MidiPlayerGlobal.CurrentMidiSet.SoundFonts.Find(s => s.Name == MidiPlayerGlobal.ImSFCurrent.SoundFontName) == null)
                    {
                        // no the current SF has been remove from resource, define first SF  as active or nothing if no SF exists
                        if (MidiPlayerGlobal.CurrentMidiSet.SoundFonts.Count >= 0)
                        {
                            MidiPlayerGlobal.CurrentMidiSet.SetActiveSoundFont(0);
                            LoadImSF();
                        }
                        else
                            MidiPlayerGlobal.CurrentMidiSet.SetActiveSoundFont(-1);
                        tobesaved = true;
                    }
                }
                if (tobesaved)
                    MidiPlayerGlobal.CurrentMidiSet.Save();
            }

        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }

        try
        {
            if (Directory.Exists(Path.Combine(Application.dataPath, MidiPlayerGlobal.PathToMidiFile)))
            {
                RenameExtFileFromMidToBytes();

                //
                // Check Midifile : remove from DB midifile removed from resource
                //
                bool tobesaved = false;
                List<string> midiFiles = GetMidiFilePath();
                int im = 0;
                while (im < MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count)
                {
                    bool found = false;
                    foreach (string filepath in midiFiles)
                        if (filepath.Contains(MidiPlayerGlobal.CurrentMidiSet.MidiFiles[im]))
                            found = true;
                    if (!found)
                    {
                        MidiPlayerGlobal.CurrentMidiSet.MidiFiles.RemoveAt(im);
                        tobesaved = true;
                    }
                    else
                        im++;
                }

                //
                // Check Midifile : Add to DB midifile found from resource
                //
                foreach (string pathmidifile in midiFiles)
                {
                    string filename = Path.GetFileNameWithoutExtension(pathmidifile);
                    if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles.FindIndex(s => s == filename) < 0)
                    {
                        tobesaved = true;
                        MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Add(filename);
                    }
                }

                if (tobesaved)
                {
                    MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Sort();
                    MidiPlayerGlobal.CurrentMidiSet.Save();
                }
            }
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    /// <summary>
    /// Renum midi files
    /// </summary>
    public static void RenumMidiFile()
    {
        try
        {
            string folder = Application.dataPath + "/" + MidiPlayerGlobal.PathToMidiFile;
            if (Directory.Exists(folder))
            {
                List<string> fileEntries = new List<string>(Directory.GetFiles(folder, "*" + MidiPlayerGlobal.ExtensionMidiFile, SearchOption.AllDirectories));
                fileEntries.Sort();
                int index = 1;
                foreach (string filename in fileEntries)
                {
                    try
                    {
                        if (!Path.GetFileName(filename).StartsWith("MPTK_Example"))
                        {
                            string target = "";
                            do
                            {
                                target = Path.Combine(Path.GetDirectoryName(filename), string.Format("MPTK_Example {0:0000}", index++)) + MidiPlayerGlobal.ExtensionMidiFile;
                            } while (File.Exists(target) && index < 10000);
                            if (index < 10000)
                            {
                                //Debug.Log("MOVE TO " + target);
                                File.Move(filename, target);
                                string meta = Path.ChangeExtension(filename, ".meta");
                                if (File.Exists(meta))
                                    File.Delete(meta);
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MidiPlayerGlobal.ErrorDetail(ex);
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    /// <summary>
    /// Load Simplified SoundFont from OS (edit mode)
    /// </summary>
    /// <param name="soundPath"></param>
    /// <param name="soundFontName"></param>
    public static void LoadImSF(string soundPath = null, string soundFontName = null)
    {
        try
        {
            if (soundPath == null || soundFontName == null)
            //Load active SF
            {
                if (MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo != null)
                {
                    soundPath = MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo.ImSFPath;
                    soundFontName = MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo.Name;
                }
            }
            if (soundPath != null && soundFontName != null)
            {
                Debug.Log("SoundFont loading " + soundFontName);
                MidiPlayerGlobal.ImSFCurrent = ImSoundFont.Load(soundPath, soundFontName);
                MidiPlayerGlobal.ImSFCurrent.CreateBankDescription();
            }
            else
                Debug.Log("LoadImSF : no sf defined");
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    private static void RenameExtFileFromMidToBytes()
    {
        try
        {
            string folder = Application.dataPath + "/" + MidiPlayerGlobal.PathToMidiFile;
            if (Directory.Exists(folder))
            {
                string[] fileEntries = Directory.GetFiles(folder, "*.mid", SearchOption.AllDirectories);
                foreach (string filename in fileEntries)
                {
                    try
                    {
                        string target = Path.ChangeExtension(filename, MidiPlayerGlobal.ExtensionMidiFile);
                        if (File.Exists(target))
                            File.Delete(target);
                        File.Move(filename, target);
                        string meta = Path.ChangeExtension(filename, ".meta");
                        if (File.Exists(meta))
                            File.Delete(meta);
                    }
                    catch (System.Exception ex)
                    {
                        MidiPlayerGlobal.ErrorDetail(ex);
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
    }

    private static List<string> GetMidiFilePath()
    {
        List<string> paths = new List<string>();
        try
        {
            string folder = Application.dataPath + "/" + MidiPlayerGlobal.PathToMidiFile;
            if (Directory.Exists(folder))
            {
                try
                {
                    string[] fileEntries = Directory.GetFiles(folder, "*" + MidiPlayerGlobal.ExtensionMidiFile, SearchOption.AllDirectories);
                    if (fileEntries.Length > 0)
                    {
                        paths = new List<string>(fileEntries);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning("Error GetMidiFilePath GetFiles " + ex.Message);
                }
            }
        }
        catch (System.Exception ex)
        {
            MidiPlayerGlobal.ErrorDetail(ex);
        }
        return paths;
    }
}
