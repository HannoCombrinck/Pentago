using UnityEngine;

public class ContextHoverClickable : InputContext
{
    private InputContext contextIdle;
    private GameObject clickableObject;

    void Awake()
    {
        contextIdle = GetComponent<ContextIdle>();
    }

    protected override void OnEnter()
    {
        SetClickable(handler.mouseRayInfo.collider.gameObject);
    }

    protected override void OnExit()
    {
        DeselectClickable();
    }

    protected override void OnHandleInput()
    {
        if (!handler.mouseOverClickable)
        {
            handler.SwitchContext(contextIdle);
            return;
        }
        else if (clickableObject != handler.mouseRayInfo.collider.gameObject)
        {
            SetClickable(handler.mouseRayInfo.collider.gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var clickable = clickableObject.GetComponent<Clickable>();
            if (clickable)
                clickable.Clicked();
        }
    }

    private void SetClickable(GameObject newClickable)
    {
        DeselectClickable();
        clickableObject = newClickable;
    }

    private void DeselectClickable()
    {
        clickableObject = null;
    }
}
