using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointer : MonoBehaviour
{
    //public LayerMask clickableLayer;
    public float maxPointerDistance = 100.0f;
    public bool overClickable { get; private set; }
    public IClickable clickable { get; private set; }
    public bool overUI { get; private set; }
    public Ray ray { get; private set; }

    private RaycastHit hitInfo;
    
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        overUI = EventSystem.current.IsPointerOverGameObject();
        if (!Physics.Raycast(ray, out hitInfo, maxPointerDistance) || overUI)
        {
            clickable = null;
            overClickable = false;
            return;
        }

        clickable = hitInfo.collider.gameObject.GetComponent<IClickable>();
        overClickable = clickable != null;
    }
}
