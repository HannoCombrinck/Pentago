using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextIdle : InputContext
{
    private InputContext contextHoverClickable;
    private InputContext contextOrbitCamera;
    private InputContext contextZoomCamera;

    void Awake()
    {
        contextHoverClickable = GetComponent<ContextHoverClickable>();
        contextOrbitCamera = GetComponent<ContextOrbitCamera>();
        contextZoomCamera = GetComponent<ContextZoomCamera>();
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Idle context");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Idle context");
    }

    public override void OnHandleInput()
    {
        if (handler.mouseOverClickable)
        {
            handler.SwitchContext(contextHoverClickable);
            return;
        }

        if (Input.GetMouseButtonDown(0))
            handler.SwitchContext(contextOrbitCamera);

        if (Input.GetMouseButtonDown(1))
            handler.SwitchContext(contextZoomCamera);
    }
}
