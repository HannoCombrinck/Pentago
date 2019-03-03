using UnityEngine;

public class QuadrantRotatorProcedural : MonoBehaviour, IQuadrantRotator
{
    public void RotateClockwise()
    {
        Debug.Log("QuadrantRotatorProcedural: Attempting procedural clockwise rotation on: " + gameObject.name);
    }

    public void RotateCounterClockwise()
    {
        Debug.Log("QuadrantRotatorProcedural: Attempting procedural counterclockwise rotation on: " + gameObject.name);
    }

    public bool IsBusyRotating()
    {
        return false;
    }
}
