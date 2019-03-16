using UnityEngine;

public class ContextZoomCamera : IInputContext
{
    public OrbitCamera cameraController;
    public float mouseSensitivityZoom = 0.25f;

    public override void OnHandleInput()
    {
        if (Input.GetMouseButtonUp(1)) // Right mouse button
        {
            handler.SwitchContext(GetComponent<ContextIdle>());
            return;
        }

        cameraController.Zoom(Input.GetAxis("Mouse Y") * mouseSensitivityZoom);
    }

}
