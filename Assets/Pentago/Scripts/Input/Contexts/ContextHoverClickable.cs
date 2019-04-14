using UnityEngine;

public class ContextHoverClickable : IInputContext
{
    private IClickable clickable;

    public override void OnHandleInput()
    {
        if (!handler.mousePointer.overClickable)
        {
            handler.SwitchContext(GetComponent<ContextDefault>());
            return;
        }

        // Handle the case where the input context hasn't changed but the mouse is 
        // pointing at a different clickable than in the previous Update().
        if (clickable != handler.mousePointer.clickable)
            SetClickable(handler.mousePointer.clickable);

        if (Input.GetMouseButtonDown(0)) // Left mouse button
            clickable.LeftClick(null);
        if (Input.GetMouseButtonDown(1)) // Right mouse button
            clickable.RightClick(null);
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
        clickable.MousePointerEnter(null);
    }

    private void DeselectClickable()
    {
        clickable?.MousePointerExit(null);
        clickable = null;
    }

}
