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

    SerializedProperty LaneSounds;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        DefenceBG = serializedObject.FindProperty("DefenceBG");
        DefenceMIDI = serializedObject.FindProperty("DefenceMIDI");
        AttackBG = serializedObject.FindProperty("AttackBG");
        AttackMIDI = serializedObject.FindProperty("AttackMIDI");

        LaneSounds = serializedObject.FindProperty("LaneSounds");

    }

    public override void OnInspectorGUI()
    {
        LevelSounds me = (LevelSounds)target;

        serializedObject.Update();
        EditorGUILayout.PropertyField(DefenceBG);

        GUILayout.Label("Midi: " + me.DefenceMIDI);
        if (GUILayout.Button("DefenceMIDI"))
        {
            string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", "Assets/0_ProjectFiles/Resources/Tracks/", "mid");
            var trimIndex = selectedFile.IndexOf("/Resources/");
            me.DefenceMIDI = selectedFile.Remove(0, trimIndex + "/Resources/".Length);
        }

        EditorGUILayout.PropertyField(AttackBG);

        GUILayout.Label("Midi: " + me.AttackMIDI);
        if (GUILayout.Button("AttackMIDI"))
        {
            string selectedFile = EditorUtility.OpenFilePanel("Open and import Midi file", "Assets/0_ProjectFiles/Resources/Tracks/", "mid");
            var trimIndex = selectedFile.IndexOf("/Resources/");
            me.AttackMIDI = selectedFile.Remove(0, trimIndex + "/Resources/".Length);
        }

        //to display array
        EditorGUI.BeginChangeCheck();
        SerializedProperty LaneSounds = serializedObject.FindProperty("LaneSounds");   
        EditorGUILayout.PropertyField(LaneSounds, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        serializedObject.ApplyModifiedProperties();
    }

}