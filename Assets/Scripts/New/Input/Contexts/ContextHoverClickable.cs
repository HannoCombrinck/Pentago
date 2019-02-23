using UnityEngine;

public class ContextHoverClickable : InputContext
{
    private GameObject clickableObject;

    public override void OnHandleInput()
    {
        if (!handler.mousePointer.overClickable)
        {
            handler.SwitchContext(GetComponent<ContextIdle>());
            return;
        }
        else if (clickableObject != handler.mousePointer.hitInfo.collider.gameObject)
        {
            SetClickable(handler.mousePointer.hitInfo.collider.gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var clickable = clickableObject.GetComponent<Clickable>();
            if (clickable)
                clickable.Clicked();
        }
    }

    public override void OnEnter()
    {
        SetClickable(handler.mousePointer.hitInfo.collider.gameObject);
    }

    public override void OnExit()
    {
        DeselectClickable();
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
