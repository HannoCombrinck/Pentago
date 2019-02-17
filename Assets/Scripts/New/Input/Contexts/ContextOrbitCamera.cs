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

    protected override void OnEnter()
    {
    }

    protected override void OnExit()
    {
    }

    protected override void OnHandleInput()
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
