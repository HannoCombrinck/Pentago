using UnityEngine;

public class ContextHoverClickable : InputContext
{
    private IClickable clickable;

    public override void OnHandleInput()
    {
        if (!handler.mousePointer.overClickable)
        {
            handler.SwitchContext(GetComponent<ContextIdle>());
            return;
        }

        // Handle the case where the input context hasn't changed but the mouse is 
        // pointing at a different clickable than in the previous Update().
        if (clickable != handler.mousePointer.clickable)
        {
            SetClickable(handler.mousePointer.clickable);
        }

        if (Input.GetMouseButtonDown(0))
            clickable.OnLeftClick();
        if (Input.GetMouseButtonDown(1))
            clickable.OnRightClick();
    }

    public override void OnEnter()
    {
        SetClickable(handler.mousePointer.clickable);
    }

    public override void OnExit()
    {
        DeselectClickable();
    }

    private void SetClickable(IClickable newClickable)
    {
        DeselectClickable();
        clickable = newClickable;
        clickable.OnMousePointerEnter();
    }

    private void DeselectClickable()
    {
        clickable?.OnMousePointerExit();
        clickable = null;
    }

}
