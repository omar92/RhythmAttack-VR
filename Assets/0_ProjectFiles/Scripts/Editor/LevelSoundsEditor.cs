using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(LevelSounds))]
[CanEditMultipleObjects]
public class LevelSoundsEditor : Editor
{
    SerializedProperty DefenceLevels;
    SerializedProperty AttackLevels;

    SerializedProperty LaneSounds;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        DefenceLevels = serializedObject.FindProperty("DefenceLevels");
        AttackLevels = serializedObject.FindProperty("AttackLevels");

        LaneSounds = serializedObject.FindProperty("LaneSounds");

    }

    public override void OnInspectorGUI()
    {
        var originalColor = GUI.color;
        LevelSounds me = (LevelSounds)target;

        serializedObject.Update();

        GUILayout.Label("Defence mode");
        if (me.DefenceLevels == null) { me.DefenceLevels = new List<LevelBgMidiMapper>(); }
        AddLevelsMapper(originalColor, DefenceLevels, me.DefenceLevels);

        GUILayout.Space(10);

        GUILayout.Label("Attack mode");
        if (me.AttackLevels == null) { me.AttackLevels = new List<LevelBgMidiMapper>(); }
        AddLevelsMapper(originalColor, AttackLevels, me.AttackLevels);

        //to display array
        EditorGUI.BeginChangeCheck();
        SerializedProperty LaneSounds = serializedObject.FindProperty("LaneSounds");
        EditorGUILayout.PropertyField(LaneSounds, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        serializedObject.ApplyModifiedProperties();
    }

    private void AddLevelsMapper(Color originalColor, SerializedProperty property, List<LevelBgMidiMapper> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i), true);
            if (list[i].MIDI!=null && list[i].MIDI.Length < 1)
            {
                GUI.color = Color.red;
                if (GUILayout.Button("Select MIDI"))
                {
                    AddMidiFile(i, list);
                }
            }
            else
            {
                if (GUILayout.Button("Midi: " + list[i].MIDI))
                {
                    AddMidiFile(i, list);
                }
            }

            GUI.color = originalColor;

            if (GUILayout.Button("Remove Level"))
            {
                var n = i;
                list.RemoveAt(n);
            }
            GUILayout.Space(10);
        }
        if (GUILayout.Button("Add Level"))
        {
            list.Add(new LevelBgMidiMapper());
            list[list.Count - 1] = new LevelBgMidiMapper(null, "");
        }
    }

    private static void AddMidiFile(int i, List<LevelBgMidiMapper> list)
    {
        string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", "Assets/0_ProjectFiles/Resources/Tracks/", "mid");
        var trimIndex = selectedFile.IndexOf("/Resources/");
        var data = list[i];
        data.MIDI = selectedFile.Remove(0, trimIndex + "/Resources/".Length);
        list[i] = data;
    }
}