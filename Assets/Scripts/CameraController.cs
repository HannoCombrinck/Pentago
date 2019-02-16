using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followObject;
    public float distance = 10f;
    public float minDistance = 1f;
    public float maxDistance = 20f;
    public float heightOffset = 0f;
    public float mouseSensitivityX = 5f;
    public float mouseSensitivityY = 5f;
    public float mouseSensitivityZoom = 0.25f;
    public float mouseSensitivityScroll = 0.5f;
    public float yaw = -30;
    public float minYaw = -360f;
    public float maxYaw = 360f;
    public float pitch = -25f;
    public float minPitch = -60f;
    public float maxPitch = 60f;

    private Camera attachedCamera;
    private Quaternion rotation;
    //private bool ignoreCameraOrbit = false;

    void Awake()
    {
        rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
        attachedCamera = GetComponent<Camera>();
    }

    void Update()
    {
        //HandleMouseInputs(); // TODO: Move this (and relevant members) out to PlayerController and provide public functions: SetPitch, SetYaw, SetDistance etc.
        UpdateCameraTransform();
    }

    public void Yaw(float angle)
    {
        yaw += angle;
        yaw = ClampAngle(yaw, minYaw, maxYaw);
        UpdateRotation();
    }

    public void Pitch(float angle)
    {
        pitch += angle;
        pitch = ClampAngle(pitch, minPitch, maxPitch);
        UpdateRotation();
    }

    public void Zoom(float deltaDistance)
    {
        distance -= deltaDistance;
    }

    private void UpdateRotation()
    {
        rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
    }

    /*public void IgnoreCameraOrbit(bool b)
    {
        ignoreCameraOrbit = b;
    }*/

    public Camera GetCamera()
    {
        return attachedCamera;
    }

    /*private void HandleMouseInputs()
    {
        if (Input.GetMouseButton(0) && !ignoreCameraOrbit) // Holding left mouse button 
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivityX;
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivityY;
            yaw = ClampAngle(yaw, minYaw, maxYaw);
            pitch = ClampAngle(pitch, minPitch, maxPitch);
            rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
        }
        else if (Input.GetMouseButton(1)) // Holding right mouse button down
        {
            distance -= Input.GetAxis("Mouse Y") * mouseSensitivityZoom;
        }
        distance -= Input.mouseScrollDelta.y * mouseSensitivityScroll;
    }*/

    private void UpdateCameraTransform()
    {
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        Vector3 camTargetPoint = followObject.transform.position + Vector3.up * heightOffset;
        transform.position = camTargetPoint + (rotation * Vector3.forward * distance);
        transform.LookAt(camTargetPoint);
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;

        return Mathf.Clamp(angle, min, max);
    }
}
