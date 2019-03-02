using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRotatorProcedural : MonoBehaviour, IBoardRotator
{
    public void RotateClockwise()
    {
        Debug.Log("BoardRotatorProcedural: Attempting procedural clockwise rotation on: " + gameObject.name);
    }

    public void RotateCounterClockwise()
    {
        Debug.Log("BoardRotatorProcedural: Attempting procedural counterclockwise rotation on: " + gameObject.name);
    }
}
