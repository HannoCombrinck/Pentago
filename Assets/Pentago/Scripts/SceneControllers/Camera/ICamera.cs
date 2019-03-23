using UnityEngine;

public abstract class ICameraController : MonoBehaviour
{
    public abstract Camera GetCamera();
    public abstract void Yaw(float angle);
    public abstract void Pitch(float angle);
    public abstract void Zoom(float angle);
}
