using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(State))]
public class GameStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = true;

        var state = target as State;
        if (GUILayout.Button("Reset"))
            state.ResetState();
    }
}
