using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameState))]
public class GameStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = true;

        GameState state = target as GameState;
        if (GUILayout.Button("Reset"))
            state.ResetState();
    }
}
