using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuadrantRotatorProcedural))]
public class RotatorProceduralEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        var rotater = target as QuadrantRotatorProcedural;
        if (GUILayout.Button("Clockwise"))
            rotater.RotateClockwise();
        if (GUILayout.Button("Counterclockwise"))
            rotater.RotateCounterClockwise();
    }
}
