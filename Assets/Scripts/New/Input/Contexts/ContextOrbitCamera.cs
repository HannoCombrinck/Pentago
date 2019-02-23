using UnityEngine;

public class ContextOrbitCamera : InputContext
{
    public CameraController cameraController;
    public float mouseSensitivityX = 5f;
    public float mouseSensitivityY = 5f;

    public override void OnHandleInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            handler.SwitchContext(GetComponent<ContextIdle>());
            return;
        }

        cameraController.Yaw(Input.GetAxis("Mouse X") * mouseSensitivityX);
        cameraController.Pitch(Input.GetAxis("Mouse Y") * mouseSensitivityY);
    }

}
