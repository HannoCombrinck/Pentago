using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardRotatorAnimated))]
public class RotatorAnimatedEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        BoardRotatorAnimated rotater = target as BoardRotatorAnimated;
        if (GUILayout.Button("Clockwise"))
            rotater.RotateClockwise();
        if (GUILayout.Button("Counterclockwise"))
            rotater.RotateCounterClockwise();
    }
}
