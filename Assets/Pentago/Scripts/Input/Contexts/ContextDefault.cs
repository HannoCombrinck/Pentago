﻿using UnityEngine;

public class ContextDefault : IInputContext
{
    public override void OnHandleInput()
    {
        if (handler.mousePointer.overUI)
            return;

        if (handler.mousePointer.overClickable)
        {
            handler.SwitchContext(GetComponent<ContextHoverClickable>());
            return;
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button
            handler.SwitchContext(GetComponent<ContextOrbitCamera>());

        if (Input.GetMouseButtonDown(1)) // Right mouse button
            handler.SwitchContext(GetComponent<ContextZoomCamera>());
    }
}
