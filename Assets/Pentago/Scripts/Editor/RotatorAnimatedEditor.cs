using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuadrantRotatorAnimated))]
public class RotatorAnimatedEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        var rotater = target as QuadrantRotatorAnimated;
        if (GUILayout.Button("Clockwise"))
            rotater.RotateClockwise();
        if (GUILayout.Button("Counterclockwise"))
            rotater.RotateCounterClockwise();
    }
}
