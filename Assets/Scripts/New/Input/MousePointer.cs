using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public LayerMask clickableLayer;
    public float maxPointerDistance = 100.0f;

    public Ray ray;
    public RaycastHit hitInfo;
    public bool overClickable;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        overClickable = Physics.Raycast(ray, out hitInfo, maxPointerDistance, clickableLayer);
    }
}
