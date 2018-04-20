//#define MPTK_PRO
using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Midi;
namespace MidiPlayerTK
{
    //using MonoProjectOptim;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Window editor for the setup of MPTK
    /// </summary>
    public class MidiPlayerSetupWindow : EditorWindow
    {
        public static string paxSite = "http://www.paxstellar.com";
        public static string blogSite = "http://s348269372.onlinehome.fr/2018/03/06/midi-player-toolkit-for-unity/";
        public static string UnitySite = "https://www.assetstore.unity3d.com/#!/content/115331";
#if MPTK_PRO
        public static string version = "1.1 Pro";
#else
        public static string version = "1.1 Free";
#endif
        public static string releaseDate = "April 14 2018";
        private static MidiPlayerSetupWindow window;

        Vector2 scrollPosBanks = Vector2.zero;
        Vector2 scrollPosSoundFont = Vector2.zero;
        Vector2 scrollPosMidiFile = Vector2.zero;
        Vector2 scrollPosAnalyze = Vector2.zero;
        Vector2 scrollPosOptim = Vector2.zero;

        static float widthLeft;
        static float widthRight;

        static float heightTop;
        static float heightMiddle;

        static float heightBottom;

        static int itemHeight;
        static int buttonWidth;
        static int buttonHeight;
        static float espace;

        static float xpostitlebox;
        static float ypostitlebox;


        string midifile;

        static GUIStyle styleBold;
        static GUIStyle styleRichText;
        static float heightLine;

        static BuilderInfo ScanInfo;
        static BuilderInfo OptimInfo;

        static bool KeepAllPatchs = false;
        static bool KeepAllZones = false;

        [MenuItem("Tools/Midi Player Toolkit Setup")]
        public static void Init()
        {
            // Get existing open window or if none, make a new one:
            try
            {
                window = (MidiPlayerSetupWindow)EditorWindow.GetWindow(typeof(MidiPlayerSetupWindow));
                window.titleContent.text = "Midi Player Setup";
                window.minSize = new Vector2(828, 565);

                styleBold = new GUIStyle(EditorStyles.boldLabel);
                styleBold.fontStyle = FontStyle.Bold;

                styleRichText = new GUIStyle(EditorStyles.label);
                styleRichText.richText = true;
                styleRichText.alignment = TextAnchor.UpperLeft;
                heightLine = styleRichText.lineHeight * 1.2f;

                espace = 5;
                widthLeft = 415;
                heightTop = 200;
                heightMiddle = 200;
                itemHeight = 25;
                buttonWidth = 150;
                buttonHeight = 18;

                xpostitlebox = 2;
                ypostitlebox = 5;
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }
      
        private void OnLostFocus()
        {
#if UNITY_2017_1_OR_NEWER
            // Trig an  error before v2017...
            if (Application.isPlaying)
            {
                window.Close();
            }
#endif
        }
        private void OnFocus()
        {
            // Load description of available soundfont
            try
            {
                MidiPlayerToolsEdit.LoadMidiSet();
                MidiPlayerToolsEdit.CheckMidiSet();
                if (MidiPlayerGlobal.ImSFCurrent != null)
                {
                    KeepAllPatchs = MidiPlayerGlobal.ImSFCurrent.KeepAllPatchs;
                    KeepAllZones = MidiPlayerGlobal.ImSFCurrent.KeepAllZones;
                }
                AssetDatabase.Refresh();
            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        void OnGUI()
        {
            try
            {
                if (window == null) Init();
                float startx = 5;
                float starty = 7;
                //Log.Write("test");

                GUIContent content = new GUIContent() { text = "Setup SoundFont and Midi files to play in your application - Version " + version, tooltip = "" };
                EditorGUI.LabelField(new Rect(startx, starty, 500, itemHeight), content, styleBold);

                GUI.color = MidiPlayerToolsEdit.ButtonColor;
                content = new GUIContent() { text = "Help & Contact", tooltip = "Get some help" };
                Rect rect = new Rect(window.position.size.x - buttonWidth - 5, starty, buttonWidth, buttonHeight);
                try
                {
                    if (GUI.Button(rect, content))
                        PopupWindow.Show(rect, new AboutMPTK());
                }
                catch (Exception)
                {
                    // generate some weird exception ...
                }

                starty += buttonHeight + espace;

                widthRight = window.position.size.x - widthLeft - 2 * espace - startx;
                heightBottom = window.position.size.y - heightTop - heightMiddle - 3 * espace - starty;

                ShowListSoundFonts(startx, starty);
                ShowListBanks(startx + widthLeft + espace, starty);
                ShowListMidiFiles(startx, starty + heightTop + espace);
                ShowMidiAnalyse(startx + widthLeft + espace, starty + heightTop + espace);
                ShowOptim(startx, starty + heightTop + espace + heightMiddle + espace);
                ShowLogOptim(startx + widthLeft + espace, starty + heightTop + espace + heightMiddle + espace);

            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Display, add, remove Soundfont
        /// </summary>
        /// <param name="localstartX"></param>
        /// <param name="localstartY"></param>
        private void ShowListSoundFonts(float localstartX, float localstartY)
        {
            try
            {
                Rect zone = new Rect(localstartX, localstartY, widthLeft, heightTop);
                GUI.color = new Color(.8f, .8f, .8f, 1f);
                GUI.Box(zone, "");
                GUI.color = Color.white;
                if (MidiPlayerGlobal.CurrentMidiSet != null && MidiPlayerGlobal.CurrentMidiSet.SoundFonts != null)
                {
                    string caption = "SoundFont available";
                    if (MidiPlayerGlobal.CurrentMidiSet.SoundFonts.Count == 0)
                    {
                        caption = "No SoundFont available yet";
                        MidiPlayerGlobal.ImSFCurrent = null;
                    }

                    GUIContent content = new GUIContent() { text = caption, tooltip = "Each SoundFont contains a set of bank of sound. \nOnly one SoundFont can be active at the same time for the midi player" };
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, localstartY + ypostitlebox, 300, itemHeight), content, styleBold);
                    Rect rect = new Rect(widthLeft - buttonWidth - espace, localstartY + ypostitlebox, buttonWidth, buttonHeight);
#if MPTK_PRO
                    if (GUI.Button(rect, "Add SoundFont"))
                    {
                        if (EditorUtility.DisplayDialog("Import SoundFont", "This action could take some minute, do you confirm ?", "Ok", "Cancel"))
                        {
                            SoundFontOptim.AddSoundFont();
                            scrollPosSoundFont = Vector2.zero;
                            KeepAllPatchs = false;
                            KeepAllZones = false;
                        }
                    }
#else
                    if (GUI.Button(rect, "Add SoundFont [PRO]"))
                    {
                        try
                        {
                            PopupWindow.Show(rect, new GetVersionPro());

                        }
                        catch (Exception)
                        {
                            // generate some weird exception ...
                        }
                    }
#endif

                    Rect listVisibleRect = new Rect(localstartX, localstartY + itemHeight, widthLeft - 5, heightTop - itemHeight - 5);
                    Rect listContentRect = new Rect(0, 0, widthLeft - 20, MidiPlayerGlobal.CurrentMidiSet.SoundFonts.Count * itemHeight + 5);

                    scrollPosSoundFont = GUI.BeginScrollView(listVisibleRect, scrollPosSoundFont, listContentRect);
                    float boxY = 0;

                    for (int i = 0; i < MidiPlayerGlobal.CurrentMidiSet.SoundFonts.Count; i++)
                    {
                        SoundFontInfo sf = MidiPlayerGlobal.CurrentMidiSet.SoundFonts[i];

                        GUI.color = new Color(.7f, .7f, .7f, 1f);
                        float boxX = 5;
                        GUI.Box(new Rect(boxX, boxY + 5, widthLeft - 30, itemHeight), "");
                        GUI.color = Color.white;

                        content = new GUIContent() { text = sf.Name, tooltip = "" };
                        EditorGUI.LabelField(new Rect(boxX, boxY + 9, 200, itemHeight), content);

                        if (sf.Name == MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo.Name)
                        {
                            GUI.color = MidiPlayerToolsEdit.ButtonColor;
                        }

                        boxX += 200 + espace;
                        if (GUI.Button(new Rect(boxX, boxY + 9, 80, buttonHeight), "Select"))
                        {

                            MidiPlayerGlobal.CurrentMidiSet.SetActiveSoundFont(i);
                            MidiPlayerToolsEdit.LoadImSF(sf.ImSFPath, sf.Name);
                            MidiPlayerGlobal.CurrentMidiSet.Save();
                            if (MidiPlayerGlobal.ImSFCurrent != null)
                            {
                                KeepAllPatchs = MidiPlayerGlobal.ImSFCurrent.KeepAllPatchs;
                                KeepAllZones = MidiPlayerGlobal.ImSFCurrent.KeepAllZones;
                            }
                        }
                        boxX += 80 + espace;

                        GUI.color = Color.white;
                        rect = new Rect(boxX, boxY + 9, 80, buttonHeight);
                        if (GUI.Button(rect, "Remove"))
                        {
#if MPTK_PRO
                            if (!string.IsNullOrEmpty(sf.ImSFPath) && EditorUtility.DisplayDialog("Delete SoundFont", "Are you sure to delete all the content of this folder ? " + sf.ImSFPath, "ok", "cancel"))
                            {
                                try
                                {
                                    Directory.Delete(sf.ImSFPath, true);
                                    File.Delete(sf.ImSFPath + ".meta");

                                }
                                catch (Exception ex)
                                {
                                    Debug.Log("Remove SF " + ex.Message);
                                }
                                AssetDatabase.Refresh();
                                MidiPlayerToolsEdit.CheckMidiSet();
                            }
#else
                            try
                            {
                                PopupWindow.Show(rect, new GetVersionPro());
                            }
                            catch (Exception)
                            {
                                // generate some weird exception ...
                            }
#endif
                        }

                        GUI.color = Color.white;
                        boxY += itemHeight;
                    }
                    GUI.EndScrollView();
                }
            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Show banks of selected SoundFont
        /// </summary>
        /// <param name="localstartX"></param>
        /// <param name="localstartY"></param>
        private void ShowListBanks(float localstartX, float localstartY)
        {
            try
            {
                Rect zone = new Rect(localstartX, localstartY, widthRight, heightTop);
                GUI.color = new Color(.8f, .8f, .8f, 1f);
                GUI.Box(zone, "");
                GUI.color = Color.white;

                if (MidiPlayerGlobal.ImSFCurrent != null && MidiPlayerGlobal.ImSFCurrent.Banks != null)
                {
                    GUIContent content = new GUIContent() { text = "Banks available in SoundFont " + MidiPlayerGlobal.ImSFCurrent.SoundFontName, tooltip = "Each bank contains a set of preset (instrument).\nOnly two banks can be active at the same time : default sound (piano, ...) and drum kit (percussive)\n\nYou must save the SoundFont after this action" };
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, localstartY + ypostitlebox, 400, itemHeight), content, styleBold);

                    // Count available banks
                    int countBank = 0;
                    foreach (ImBank bank in MidiPlayerGlobal.ImSFCurrent.Banks)
                        if (bank != null) countBank++;
                    Rect listVisibleRect = new Rect(localstartX, localstartY + itemHeight, widthRight, heightTop - itemHeight - 5);
                    Rect listContentRect = new Rect(0, 0, widthRight - 15, countBank * itemHeight + 5);

                    scrollPosBanks = GUI.BeginScrollView(listVisibleRect, scrollPosBanks, listContentRect);

                    float boxY = 0;
                    SoundFontInfo sfi = MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo;
                    foreach (ImBank bank in MidiPlayerGlobal.ImSFCurrent.Banks)
                    {
                        if (bank != null)
                        {
                            GUI.color = new Color(.7f, .7f, .7f, 1f);
                            GUI.Box(new Rect(5, boxY + 5, widthRight - 25, itemHeight), "");

                            GUI.color = Color.white;

                            content = new GUIContent() { text = string.Format("Bank number {0,3}", bank.BankNumber), tooltip = bank.Description };
                            EditorGUI.LabelField(new Rect(10, boxY + 9, 120, itemHeight), content);

                            if (GUI.Toggle(new Rect(150, boxY + 9, 100, buttonHeight), sfi.DefaultBankNumber == bank.BankNumber, "Default bank"))
                            {
                                sfi.DefaultBankNumber = bank.BankNumber;
                                MidiPlayerGlobal.CurrentMidiSet.Save();
                            }

                            if (GUI.Toggle(new Rect(270, boxY + 9, 100, buttonHeight), sfi.DrumKitBankNumber == bank.BankNumber, "Drum Kit bank"))
                            {
                                sfi.DrumKitBankNumber = bank.BankNumber;
                                MidiPlayerGlobal.CurrentMidiSet.Save();
                            }

                            boxY += itemHeight;
                        }
                    }

                    GUI.EndScrollView();
                }
                else
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, localstartY + ypostitlebox, 300, itemHeight), "No SoundFont selected", styleBold);
            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }


        /// <summary>
        /// Display, add, remove Midi file
        /// </summary>
        /// <param name="localstartX"></param>
        /// <param name="localstartY"></param>
        private void ShowListMidiFiles(float localstartX, float localstartY)
        {
            try
            {
                Rect zone = new Rect(localstartX, localstartY, widthLeft, heightMiddle);
                GUI.color = new Color(.8f, .8f, .8f, 1f);
                GUI.Box(zone, "");
                GUI.color = Color.white;

                string caption = "Midi file available";
                if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles == null || MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count == 0)
                {
                    caption = "No Midi file available yet";
                    ScanInfo = new BuilderInfo();
                    OptimInfo = new BuilderInfo();
                }

                GUIContent content = new GUIContent() { text = caption, tooltip = "" };
                EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, localstartY + ypostitlebox, 300, itemHeight), content, styleBold);

                if (GUI.Button(new Rect(widthLeft - buttonWidth - espace, localstartY + ypostitlebox, buttonWidth, buttonHeight), "Add Midi file"))
                    AddMidifile();

                if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles != null)
                {
                    Rect listVisibleRect = new Rect(localstartX, localstartY + itemHeight, widthLeft - 5, heightMiddle - itemHeight - 5);
                    Rect listContentRect = new Rect(0, 0, widthLeft - 20, MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count * itemHeight + 5);

                    scrollPosMidiFile = GUI.BeginScrollView(listVisibleRect, scrollPosMidiFile, listContentRect);
                    float boxY = 0;

                    for (int i = 0; i < MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count; i++)
                    {
                        GUI.color = new Color(.7f, .7f, .7f, 1f);
                        float boxX = 5;
                        GUI.Box(new Rect(boxX, boxY + 5, widthLeft - 30, itemHeight), "");
                        GUI.color = Color.white;

                        content = new GUIContent() { text = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[i], tooltip = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[i] };
                        EditorGUI.LabelField(new Rect(boxX, boxY + 9, 200, itemHeight), content);

                        boxX += 200 + espace;
                        if (GUI.Button(new Rect(boxX, boxY + 9, 80, buttonHeight), "Analyse"))
                        {
                            ScanInfo = new BuilderInfo();
                            midifile = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[i];
                            ScanInfo.Add(midifile);
                            MidiScan.GeneralInfo(midifile, ScanInfo);
                            scrollPosAnalyze = Vector2.zero;
                        }
                        boxX += 80 + espace;

                        GUI.color = Color.white;

                        if (GUI.Button(new Rect(boxX, boxY + 9, 80, buttonHeight), "Remove"))
                        {
                            if (!string.IsNullOrEmpty(MidiPlayerGlobal.CurrentMidiSet.MidiFiles[i]))
                            {
                                DeleteResource(MidiLoad.BuildOSPath(MidiPlayerGlobal.CurrentMidiSet.MidiFiles[i]));
                                AssetDatabase.Refresh();
                                MidiPlayerToolsEdit.CheckMidiSet();
                            }
                        }
                        boxY += itemHeight;
                    }
                    GUI.EndScrollView();
                }
            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Display analyse of midifile
        /// </summary>
        /// <param name="localstartX"></param>
        /// <param name="localstartY"></param>
        private void ShowMidiAnalyse(float localstartX, float localstartY)
        {
            try
            {
                Rect zone = new Rect(localstartX, localstartY, widthRight, heightMiddle);
                GUI.color = new Color(.8f, .8f, .8f, 1f);
                GUI.Box(zone, "");
                GUI.color = Color.white;

                if (ScanInfo != null)
                {
                    Rect listVisibleRect = new Rect(localstartX, localstartY, widthRight, heightMiddle - 5);
                    Rect listContentRect = new Rect(0, 0, widthRight - 15, ScanInfo.Count * heightLine + 5);

                    scrollPosAnalyze = GUI.BeginScrollView(listVisibleRect, scrollPosAnalyze, listContentRect);
                    GUI.color = new Color(.8f, .8f, .8f, 1f);

                    float labelY = -heightLine;
                    foreach (string s in ScanInfo.Infos)
                        EditorGUI.LabelField(new Rect(0, labelY += heightLine, widthRight, heightLine), s, styleRichText);

                    GUI.color = Color.white;

                    GUI.EndScrollView();
                }
                else
                {
                    GUIContent content = new GUIContent() { text = "No Midi file analysed", tooltip = "" };
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, localstartY + ypostitlebox, 300, itemHeight), content, styleBold);
                }
            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Display optimization
        /// </summary>
        /// <param name="localstartX"></param>
        /// <param name="localstartY"></param>
        private void ShowOptim(float localstartX, float localstartY)
        {
            try
            {
                Rect zone = new Rect(localstartX, localstartY, widthLeft, heightBottom);
                GUI.color = new Color(.8f, .8f, .8f, 1f);
                GUI.Box(zone, "");
                GUI.color = Color.white;

                string tooltip = "Remove all banks and Preset not used ine the Midi file list";

                GUIContent content;
                if (MidiPlayerGlobal.ImSFCurrent != null)
                {
                    float xpos = localstartX + xpostitlebox;
                    float ypos = localstartY + ypostitlebox;
                    content = new GUIContent() { text = "Analyse Midi files and optimize SoundFont " + MidiPlayerGlobal.ImSFCurrent.SoundFontName, tooltip = tooltip };
                    EditorGUI.LabelField(new Rect(xpos, ypos, 380, itemHeight), content, styleBold);

                    int delta = 110;
                    ypos += itemHeight + espace;
                    KeepAllZones = GUI.Toggle(new Rect(xpos, ypos, delta, itemHeight), KeepAllZones, new GUIContent("Keep all Zones", "Keep all Waves associated with a Patch regardless of notes and velocities played in Midi files.\n Usefull if you want transpose Midi files."));
                    xpos += delta + espace;
                    KeepAllPatchs = GUI.Toggle(new Rect(xpos, ypos, delta, itemHeight), KeepAllPatchs, new GUIContent("Keep all Patchs", "Keep all Patchs and waves found in the SoundFont selected.\nWarning : a huge volume of files coud be created"));
                    xpos += delta + espace;
                    Rect rect = new Rect(xpos, ypos, 175, buttonHeight);
#if MPTK_PRO
                    if (GUI.Button(rect, "Analyse and Optimize"))
                    {
                        OptimInfo = new BuilderInfo();
                        SoundFontOptim.OptimizeSF(OptimInfo, KeepAllPatchs, KeepAllZones);
                    }
#else
                    if (GUI.Button(rect, "Analyse and Optimize [PRO]"))
                    {
                        try
                        {
                            PopupWindow.Show(rect, new GetVersionPro());
                        }
                        catch (Exception)
                        {
                            // generate some weird exception ...
                        }
                }
#endif
                    ypos += itemHeight + espace;

                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, ypos, 100, heightLine), "Patch count :");
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox + 110, ypos, 100, heightLine), MidiPlayerGlobal.ImSFCurrent.PatchCount.ToString());
                    ypos += heightLine;

                    string strSize = "";
                    if (MidiPlayerGlobal.ImSFCurrent.WaveSize < 1000000)
                        strSize = Math.Round((double)MidiPlayerGlobal.ImSFCurrent.WaveSize / 1000d).ToString() + " Ko";
                    else
                        strSize = Math.Round((double)MidiPlayerGlobal.ImSFCurrent.WaveSize / 1000000d).ToString() + " Mo";

                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, ypos, 100, heightLine), "Wave count :");
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox + 110, ypos, 100, heightLine), MidiPlayerGlobal.ImSFCurrent.WaveCount.ToString());
                    ypos += heightLine;

                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, ypos, 100, heightLine), "Wave size :");
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox + 110, ypos, 100, heightLine), strSize);
                }
                else
                {
                    content = new GUIContent() { text = "No SoundFont selected", tooltip = tooltip };
                    EditorGUI.LabelField(new Rect(localstartX + xpostitlebox, localstartY + ypostitlebox, 300, itemHeight), content, styleBold);
                }
            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        /// <summary>
        /// Display optimization log
        /// </summary>
        /// <param name="localstartX"></param>
        /// <param name="localstartY"></param>
        private void ShowLogOptim(float localstartX, float localstartY)
        {
            try
            {
                Rect zone = new Rect(localstartX, localstartY, widthRight, heightBottom);
                GUI.color = new Color(.8f, .8f, .8f, 1f);
                GUI.Box(zone, "");
                GUI.color = Color.white;

                if (OptimInfo != null)
                {
                    Rect listVisibleRect = new Rect(localstartX, localstartY, widthRight, heightBottom - 5);
                    Rect listContentRect = new Rect(0, 0, widthRight - 15, OptimInfo.Count * heightLine + 5);

                    scrollPosOptim = GUI.BeginScrollView(listVisibleRect, scrollPosOptim, listContentRect);
                    GUI.color = Color.white;
                    float labelY = -heightLine;
                    foreach (string s in OptimInfo.Infos)
                        EditorGUI.LabelField(new Rect(0, labelY += heightLine, widthRight, heightLine), s, styleRichText);
                    GUI.EndScrollView();
                }
            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }


        /// <summary>
        /// Add a new Midi file from desktop
        /// </summary>
        private static void AddMidifile()
        {
            try
            {
                string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", MidiPlayerToolsEdit.lastDirectoryMidi, "mid");
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    MidiPlayerToolsEdit.lastDirectoryMidi = Path.GetDirectoryName(selectedFile);

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
                AssetDatabase.Refresh();
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }

        static private void DeleteResource(string filepath)
        {
            try
            {
                Debug.Log("Delete " + filepath);
                File.Delete(filepath);
                // delete also meta
                string meta = filepath + ".meta";
                Debug.Log("Delete " + meta);
                File.Delete(meta);

            }
            catch (Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }
    }

}