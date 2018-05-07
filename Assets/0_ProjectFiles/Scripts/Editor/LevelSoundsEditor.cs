using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelSounds))]
[CanEditMultipleObjects]
public class LevelSoundsEditor : Editor
{
    SerializedProperty DefenceBG;
    SerializedProperty DefenceMIDI;
    SerializedProperty AttackBG;
    SerializedProperty AttackMIDI;

    SerializedProperty LaneSound1;
    SerializedProperty LaneSound2;
    SerializedProperty LaneSound3;
    SerializedProperty LaneSound4;
    SerializedProperty LaneSound5;
    SerializedProperty LaneSound6;
    SerializedProperty LaneSound7;
    SerializedProperty LaneSound8;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        DefenceBG = serializedObject.FindProperty("DefenceBG");
        DefenceMIDI = serializedObject.FindProperty("DefenceMIDI");
        AttackBG = serializedObject.FindProperty("AttackBG");
        AttackMIDI = serializedObject.FindProperty("AttackMIDI");

        LaneSound1 = serializedObject.FindProperty("LaneSound1");
        LaneSound2 = serializedObject.FindProperty("LaneSound2");
        LaneSound3 = serializedObject.FindProperty("LaneSound3");
        LaneSound4 = serializedObject.FindProperty("LaneSound4");
        LaneSound5 = serializedObject.FindProperty("LaneSound5");
        LaneSound6 = serializedObject.FindProperty("LaneSound6");
        LaneSound7 = serializedObject.FindProperty("LaneSound7");
        LaneSound8 = serializedObject.FindProperty("LaneSound8");
    }

    public override void OnInspectorGUI()
    {
        LevelSounds me = (LevelSounds)target;

        serializedObject.Update();
        EditorGUILayout.PropertyField(DefenceBG);

        GUILayout.Label("Midi: " + me.DefenceMIDI);
        if (GUILayout.Button("DefenceMIDI"))
        {
            string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", "Assets/Game/Resources/Tracks/", "mid");
            me.DefenceMIDI = selectedFile;
        }

        EditorGUILayout.PropertyField(AttackBG);

        GUILayout.Label("Midi: " + me.AttackMIDI);
        if (GUILayout.Button("AttackMIDI"))
        {
            string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", "Assets/Game/Resources/Tracks/", "mid");
            me.AttackMIDI = selectedFile;
        }

        EditorGUILayout.PropertyField(LaneSound1);
        EditorGUILayout.PropertyField(LaneSound2);
        EditorGUILayout.PropertyField(LaneSound3);
        EditorGUILayout.PropertyField(LaneSound4);
        EditorGUILayout.PropertyField(LaneSound5);
        EditorGUILayout.PropertyField(LaneSound6);
        EditorGUILayout.PropertyField(LaneSound7);
        EditorGUILayout.PropertyField(LaneSound8);




        serializedObject.ApplyModifiedProperties();
    }

}