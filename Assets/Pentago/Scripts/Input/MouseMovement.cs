using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public bool mouseMoved;
    //public Vector2 
    private Vector3 previousMousePosition;

    void Start()
    {
        previousMousePosition = Input.mousePosition;
    }

    void Update()
    {
        var mouseDelta = Input.mousePosition - previousMousePosition;
        previousMousePosition = Input.mousePosition;
        mouseMoved = mouseDelta.magnitude > 0.0f;
    }
}
