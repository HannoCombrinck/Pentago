using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextOrbitCamera : InputContext
{
    public CameraController cameraController;
    public float mouseSensitivityX = 5f;
    public float mouseSensitivityY = 5f;

    private InputContext idleContext;
    
    void Awake()
    {
        idleContext = GetComponent<ContextIdle>();
    }

    public override void OnEnter()
    {
        Debug.Log("Entering orbit camera context");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting orbit camera context");
    }

    public override void OnHandleInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            handler.SwitchContext(idleContext);
            return;
        }

        cameraController.Yaw(Input.GetAxis("Mouse X") * mouseSensitivityX);
        cameraController.Pitch(Input.GetAxis("Mouse Y") * mouseSensitivityY);
    }

}
