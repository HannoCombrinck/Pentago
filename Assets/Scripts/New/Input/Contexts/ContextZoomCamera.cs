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

    protected override void OnEnter()
    {
    }

    protected override void OnExit()
    {
    }

    protected override void OnHandleInput()
    {
        if (Input.GetMouseButtonUp(1))
        {
            handler.SwitchContext(idleContext);
            return;
        }

        cameraController.Zoom(Input.GetAxis("Mouse Y") * mouseSensitivityZoom);
    }

}
