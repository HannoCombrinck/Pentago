using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardRotatorProcedural))]
public class RotatorProceduralEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        BoardRotatorProcedural rotater = target as BoardRotatorProcedural;
        if (GUILayout.Button("Clockwise"))
            rotater.RotateClockwise();
        if (GUILayout.Button("Counterclockwise"))
            rotater.RotateCounterClockwise();
    }
}
