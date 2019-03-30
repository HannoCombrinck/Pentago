using UnityEngine;

public class MousePointer : MonoBehaviour
{
    //public LayerMask clickableLayer;
    public float maxPointerDistance = 100.0f;
    public bool overClickable { get; private set; }
    public IClickable clickable { get; private set; }
    public Ray ray { get; private set; }
    [HideInInspector]
    public RaycastHit hitInfo;
    
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //var overGameObject = !Physics.Raycast(ray, out hitInfo, maxPointerDistance, clickableLayer);
        if (!Physics.Raycast(ray, out hitInfo, maxPointerDistance))
        {
            clickable = null;
            overClickable = false;
            return;
        }

        clickable = hitInfo.collider.gameObject.GetComponent<IClickable>();
        overClickable = clickable != null;
    }
}
