using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(LevelSounds))]
[CanEditMultipleObjects]
public class LevelSoundsEditor : Editor
{
    SerializedProperty Level1_D_BG;
    SerializedProperty Level2_D_BG;
    SerializedProperty Level3_D_BG;
    SerializedProperty Level1_A_BG;
    SerializedProperty LaneSounds;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        Level1_D_BG = serializedObject.FindProperty("Level1_D_BG");
        Level2_D_BG = serializedObject.FindProperty("Level2_D_BG");
        Level3_D_BG = serializedObject.FindProperty("Level3_D_BG");
        Level1_A_BG = serializedObject.FindProperty("Level1_A_BG");
        
        LaneSounds = serializedObject.FindProperty("LaneSounds");
    }
    Color originalUiColor;
    public override void OnInspectorGUI()
    {
        originalUiColor = GUI.color;
        LevelSounds me = (LevelSounds)target;
        serializedObject.Update();

        //Defence ----------------------------------------------------------------------
        GUILayout.Label("Defence mode");
        //Level 1
        EditorGUILayout.PropertyField(Level1_D_BG, true);
        AddMidiButton(ref me.Level1_D_MIDI);

        GUILayout.Space(5);
        //Level 2
        EditorGUILayout.PropertyField(Level3_D_BG, true);
        AddMidiButton(ref me.Level2_D_MIDI);

        GUILayout.Space(5);
        //Level 3
        EditorGUILayout.PropertyField(Level2_D_BG, true);
        AddMidiButton(ref me.Level3_D_MIDI);

        GUILayout.Space(10);
        //Attack ----------------------------------------------------------------------
        GUILayout.Label("Attack mode");
        //Level 1
        EditorGUILayout.PropertyField(Level1_A_BG, true);
        AddMidiButton(ref me.Level1_A_MIDI);
        GUILayout.Space(10);
        //Lanes ----------------------------------------------------------------------
        EditorGUI.BeginChangeCheck();
        SerializedProperty LaneSounds = serializedObject.FindProperty("LaneSounds");
        EditorGUILayout.PropertyField(LaneSounds, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        serializedObject.ApplyModifiedProperties();
    }

    string btnText;
    private void AddMidiButton(ref string levelX_MIDI)
    {
        btnText = (levelX_MIDI.Length>0)?("Midi: " + levelX_MIDI) : "Select MIDI";
        if(levelX_MIDI.Length==0) GUI.color = Color.red;
        if (GUILayout.Button(btnText))
        {
            levelX_MIDI = SelectMidiFile();
        }
        GUI.color = originalUiColor;
    }

    private string  SelectMidiFile ()
    {
        string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", "Assets/0_ProjectFiles/Resources/Tracks/", "mid");
        var trimIndex = selectedFile.IndexOf("/Resources/");
       return selectedFile.Remove(0, trimIndex + "/Resources/".Length);
    }
}
//public class LevelSoundsEditor : Editor
//{
//    SerializedProperty DefenceLevels;
//    SerializedProperty AttackLevels;

//    SerializedProperty LaneSounds;

//    void OnEnable()
//    {
//        // Setup the SerializedProperties.
//        DefenceLevels = serializedObject.FindProperty("DefenceLevels");
//        AttackLevels = serializedObject.FindProperty("AttackLevels");

//        LaneSounds = serializedObject.FindProperty("LaneSounds");

//    }

//    public override void OnInspectorGUI()
//    {
//        var originalColor = GUI.color;
//        LevelSounds me = (LevelSounds)target;

//        serializedObject.Update();

//        GUILayout.Label("Defence mode");
//        if (me.DefenceLevels == null) { me.DefenceLevels = new List<LevelBgMidiMapper>(); }
//        AddLevelsMapper(originalColor, DefenceLevels,ref me.DefenceLevels);

//        GUILayout.Space(10);

//        GUILayout.Label("Attack mode");
//        if (me.AttackLevels == null) { me.AttackLevels = new List<LevelBgMidiMapper>(); }
//        AddLevelsMapper(originalColor, AttackLevels,ref me.AttackLevels);

//        //to display array
//        EditorGUI.BeginChangeCheck();
//        SerializedProperty LaneSounds = serializedObject.FindProperty("LaneSounds");
//        EditorGUILayout.PropertyField(LaneSounds, true);
//        if (EditorGUI.EndChangeCheck())
//            serializedObject.ApplyModifiedProperties();

//        serializedObject.ApplyModifiedProperties();
//    }

//    private void AddLevelsMapper(Color originalColor, SerializedProperty property,ref List<LevelBgMidiMapper> list)
//    {
//        for (int i = 0; i < list.Count; i++)
//        {
//            EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i), true);
//            if (list[i].MIDI!=null && list[i].MIDI.Length < 1)
//            {
//                GUI.color = Color.red;
//                if (GUILayout.Button("Select MIDI"))
//                {
//                    AddMidiFile(i,ref list);
//                    serializedObject.ApplyModifiedProperties();
//                }
//            }
//            else
//            {
//                if (GUILayout.Button("Midi: " + list[i].MIDI))
//                {
//                    AddMidiFile(i,ref list);
//                    serializedObject.ApplyModifiedProperties();
//                }
//            }

//            GUI.color = originalColor;

//            if (GUILayout.Button("Remove Level"))
//            {
//                var n = i;
//                list.RemoveAt(n);
//                serializedObject.ApplyModifiedProperties();
//            }
//            GUILayout.Space(10);
//        }
//        if (GUILayout.Button("Add Level"))
//        {
//            list.Add(new LevelBgMidiMapper());
//            list[list.Count - 1] = new LevelBgMidiMapper(null, "");
//            serializedObject.ApplyModifiedProperties();
//        }
//    }

//    private static void AddMidiFile(int i,ref List<LevelBgMidiMapper> list)
//    {
//        string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", "Assets/0_ProjectFiles/Resources/Tracks/", "mid");
//        var trimIndex = selectedFile.IndexOf("/Resources/");
//        var data = list[i];
//        data.MIDI = selectedFile.Remove(0, trimIndex + "/Resources/".Length);
//        list[i] = data;
//    }
//}