using UnityEngine;

public class ContextOrbitCamera : IInputContext
{
    public OrbitCamera cameraController;
    public float mouseSensitivityX = 5f;
    public float mouseSensitivityY = 5f;

    public override void OnHandleInput()
    {
        if (Input.GetMouseButtonUp(0)) // Left mouse button
        {
            handler.SwitchContext(GetComponent<ContextDefault>());
            return;
        }

        cameraController.Yaw(Input.GetAxis("Mouse X") * mouseSensitivityX);
        cameraController.Pitch(Input.GetAxis("Mouse Y") * mouseSensitivityY);
    }

}
