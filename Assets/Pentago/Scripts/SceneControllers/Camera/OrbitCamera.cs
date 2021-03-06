﻿using UnityEngine;

public class OrbitCamera : ICameraController
{
    public GameObject orbitObject;
    public float distance = 10f;
    public float minDistance = 1f;
    public float maxDistance = 20f;
    public float heightOffset = 0f;
    public float yaw = -30;
    public float minYaw = -360f;
    public float maxYaw = 360f;
    public float pitch = 25f;
    public float minPitch = -60f;
    public float maxPitch = 60f;

    private Quaternion rotation;

    void Awake()
    {
        rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
    }

    void Update()
    {
        UpdateCameraTransform();
    }

    public override void Yaw(float angle)
    {
        yaw += angle;
        yaw = ClampAngle(yaw, minYaw, maxYaw);
        UpdateRotation();
    }

    public override void Pitch(float angle)
    {
        pitch -= angle;
        pitch = ClampAngle(pitch, minPitch, maxPitch);
        UpdateRotation();
    }

    public override void Zoom(float deltaDistance)
    {
        distance -= deltaDistance;
    }

    private void UpdateRotation()
    {
        rotation = Quaternion.AngleAxis(yaw, Vector3.up) * Quaternion.AngleAxis(pitch, Vector3.right);
    }

    private void UpdateCameraTransform()
    {
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        Vector3 camTargetPoint = orbitObject.transform.position + Vector3.up * heightOffset;
        transform.position = camTargetPoint + (rotation * Vector3.back * distance);
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
