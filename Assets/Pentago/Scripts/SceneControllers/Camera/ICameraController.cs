using UnityEngine;

// A CameraController provides an interface for controlling a Camera in a specific way e.g. OrbitCamera, FollowCamera etc.
public abstract class ICameraController : MonoBehaviour
{
    public abstract void Yaw(float angle);
    public abstract void Pitch(float angle);
    public abstract void Zoom(float angle);
}
