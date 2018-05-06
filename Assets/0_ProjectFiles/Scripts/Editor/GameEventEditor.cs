using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor {

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GameEvent me = (GameEvent)target;
        if (GUILayout.Button("Raise"))
        {
            me.Raise();
        }
    }
}
