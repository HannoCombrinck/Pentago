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
            clickable.LeftClick(handler.playerInControl);
        if (Input.GetMouseButtonDown(1)) // Right mouse button
            clickable.RightClick(handler.playerInControl);
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
        clickable.MousePointerEnter(handler.playerInControl);
    }

    private void DeselectClickable()
    {
        clickable?.MousePointerExit(handler.playerInControl);
        clickable = null;
    }

}
