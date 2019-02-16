using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextZoomCamera : InputContext
{
    public CameraController cameraController;
    public float mouseSensitivityZoom = 0.25f;

    private InputContext idleContext;

    void Awake()
    {
        idleContext = GetComponent<ContextIdle>();
    }

    public override void OnEnter()
    {
        Debug.Log("Entering zoom camera context");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting zoom camera context");
    }

    public override void OnHandleInput()
    {
        if (Input.GetMouseButtonUp(1))
        {
            handler.SwitchContext(idleContext);
            return;
        }

        cameraController.Zoom(Input.GetAxis("Mouse Y") * mouseSensitivityZoom);
    }

}
