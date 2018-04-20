using UnityEngine;
using UnityEditor;

using System;

using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MidiPlayerTK
{
    /// <summary>
    /// Inspector for the midi global player component
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MidiFilePlayer))]
    public class MidiFilePlayerEditor : Editor
    {
        private SerializedProperty CustomEventStartPlayMidi;
        private SerializedProperty CustomEventEndPlayMidi;

        private static MidiFilePlayer instance;

        private static bool showEvents = false;
        //                                  Level=0            1           2           4             8      
        private string[] popupQuantization = { "None", "Quarter Note", "Eighth Note", "16th Note", "32th Note", "64th Note" };

        void OnEnable()
        {
            try
            {
                //Debug.Log("OnEnable MidiFilePlayerEditor");
                CustomEventStartPlayMidi = serializedObject.FindProperty("OnEventStartPlayMidi");
                CustomEventEndPlayMidi = serializedObject.FindProperty("OnEventEndPlayMidi");

                instance = (MidiFilePlayer)target;
                // Load description of available soundfont
                if (MidiPlayerGlobal.CurrentMidiSet == null || MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo == null)
                {
                    MidiPlayerToolsEdit.LoadMidiSet();
                    MidiPlayerToolsEdit.CheckMidiSet();
                }
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }
        //private void OnSceneGUI()
        //{
        //    Debug.Log("OnSceneGUI");
        //    if (MidiPlayerGlobal.CurrentMidiSet != null && MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo != null)
        //    {
        //    }
        //}


        public override void OnInspectorGUI()
        {
            try
            {
                GUI.changed = false;
                GUI.color = Color.white;

                //mDebug.Log(Event.current.type);

                string soundFontSelected = "No SoundFont selected.";
                if (MidiPlayerGlobal.CurrentMidiSet != null && MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo != null)
                {
                    soundFontSelected = MidiPlayerGlobal.CurrentMidiSet.ActiveSounFontInfo.Name;
                    EditorGUILayout.LabelField(new GUIContent("SoundFont: " + soundFontSelected, "Define SoundFont from the menu Tools/Midi Player Toolkit"));

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(new GUIContent("Midi ", "Select Midi File to play"), GUILayout.Width(150));
                    //if (GUILayout.Button(new GUIContent("Refresh", "Reload all Midi from resource folder"))) MidiPlayerGlobal.CheckMidiSet();
                    if (MidiPlayerGlobal.CurrentMidiSet.MidiFiles != null && MidiPlayerGlobal.CurrentMidiSet.MidiFiles.Count > 0)
                    {
                        // Search index from midi name
                        int selectedMidi = instance.MPTK_MidiIndex;
                        if (selectedMidi < 0)
                        {
                            selectedMidi = 0;
                            instance.MPTK_MidiName = MidiPlayerGlobal.CurrentMidiSet.MidiFiles[selectedMidi];
                        }
                        int newSelectMidi = EditorGUILayout.Popup(selectedMidi, MidiPlayerGlobal.CurrentMidiSet.MidiFiles.ToArray());
                        // Is midifile has changed ?
                        if (newSelectMidi != selectedMidi)
                        {
                            instance.MPTK_MidiIndex = newSelectMidi;
                            instance.MPTK_RePlay();
                        }
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No Midi file defined");
                        instance.MPTK_MidiName = "";
                    }
                    EditorGUILayout.EndHorizontal();

                    float volume = EditorGUILayout.Slider(new GUIContent("Volume", "Set global volume for this midi playing"), instance.MPTK_Volume, 0f, 1f);
                    if (instance.MPTK_Volume != volume)
                        instance.MPTK_Volume = volume;


                    instance.MPTK_PlayOnStart = EditorGUILayout.Toggle(new GUIContent("Play On Start", "Start playing midi when component starts"), instance.MPTK_PlayOnStart);

                    EditorGUILayout.BeginHorizontal();
                    string tooltipDistance = "Pause playing if distance between AudioListener and this component is greater than MaxDistance";
                    instance.MPTK_PauseOnDistance = EditorGUILayout.Toggle(new GUIContent("Pause With Distance", tooltipDistance), instance.MPTK_PauseOnDistance);
                    EditorGUILayout.LabelField(new GUIContent("Current:" + Math.Round(instance.distanceEditorModeOnly, 2), tooltipDistance));
                    EditorGUILayout.EndHorizontal();

                    float distance = EditorGUILayout.Slider(new GUIContent("Max Distance", tooltipDistance), instance.MPTK_MaxDistance, 0f, 500f);
                    if (instance.MPTK_MaxDistance != distance)
                        instance.MPTK_MaxDistance = distance;

                    instance.MPTK__Loop = EditorGUILayout.Toggle(new GUIContent("Loop", "Enable loop on midi play"), instance.MPTK__Loop);


                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(new GUIContent("Quantization", ""), GUILayout.Width(150));
                    int newLevel = EditorGUILayout.Popup(instance.MPTK_Quantization, popupQuantization);
                    if (newLevel != instance.MPTK_Quantization && newLevel >= 0 && newLevel < popupQuantization.Length)
                        instance.MPTK_Quantization = newLevel;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Tempo", Math.Round(instance.MPTK_Tempo, 0).ToString());
                    instance.MPTK_EnableChangeTempo = EditorGUILayout.Toggle(new GUIContent("Enable Change", "Enable tempo change when playing"), instance.MPTK_EnableChangeTempo);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Speed");
                    float speed = EditorGUILayout.Slider(instance.MPTK_Speed, 0.1f, 5f);
                    if (instance.MPTK_Speed != speed)
                        instance.MPTK_Speed = speed;
                    EditorGUILayout.EndHorizontal();


                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Transpose");
                    instance.MPTK_Transpose = EditorGUILayout.IntSlider(instance.MPTK_Transpose, -24, 24);
                    EditorGUILayout.EndHorizontal();

                    instance.MPTK_LogWaves = EditorGUILayout.Toggle(new GUIContent("Log Waves", "Log information about wave for each notes played"), instance.MPTK_LogWaves);

                    if (EditorApplication.isPlaying)
                    {

                        EditorGUILayout.Separator();
                        instance.MPTK_LogEvents = EditorGUILayout.Toggle(new GUIContent("Log Events", "Log information about each midi events read"), instance.MPTK_LogEvents);
                        EditorGUILayout.LabelField("Time", instance.playTimeEditorModeOnly + " / " + instance.durationEditorModeOnly);

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("Position");
                        float pos = EditorGUILayout.Slider(instance.MPTK_Position, 0f, (float)instance.MPTK_Duration.TotalMilliseconds);
                        if (instance.MPTK_Position != pos)
                            instance.MPTK_Position = pos;
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();

                        if (instance.MPTK_IsPlaying && !instance.MPTK__IsPaused)
                            GUI.color = MidiPlayerToolsEdit.ButtonColor;
                        if (GUILayout.Button(new GUIContent("Play", "")))
                            instance.MPTK_Play();
                        GUI.color = Color.white;

                        if (instance.MPTK__IsPaused)
                            GUI.color = MidiPlayerToolsEdit.ButtonColor;
                        if (GUILayout.Button(new GUIContent("Pause", "")))
                            if (instance.MPTK__IsPaused)
                                instance.MPTK_Play();
                            else
                                instance.MPTK_Pause();
                        GUI.color = Color.white;

                        if (GUILayout.Button(new GUIContent("Stop", "")))
                            instance.MPTK_Stop();
                        if (GUILayout.Button(new GUIContent("Restart", "")))
                            instance.MPTK_RePlay();
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button(new GUIContent("Previous", "")))
                            instance.MPTK_Previous();
                        if (GUILayout.Button(new GUIContent("Next", "")))
                            instance.MPTK_Next();
                        EditorGUILayout.EndHorizontal();

                    }

                    showEvents = EditorGUILayout.Foldout(showEvents, "Show Events");
                    if (showEvents)
                    {
                        EditorGUILayout.PropertyField(CustomEventStartPlayMidi);
                        EditorGUILayout.PropertyField(CustomEventEndPlayMidi);
                        serializedObject.ApplyModifiedProperties();
                    }

                    //showDefault = EditorGUILayout.Foldout(showDefault, "Show default editor");
                    //if (showDefault) DrawDefaultInspector();
                }
                else
                {
                    EditorGUILayout.LabelField(new GUIContent("SoundFont: " + soundFontSelected, "Define SoundFont from the menu Tools/Midi Player Toolkit"));
                    MidiPlayerToolsEdit.LoadMidiSet();
                    MidiPlayerToolsEdit.CheckMidiSet();
                }

                if (GUI.changed) EditorUtility.SetDirty(instance);
            }
            catch (System.Exception ex)
            {
                MidiPlayerGlobal.ErrorDetail(ex);
            }
        }
        //private static bool showDefault = false;


    }

}
