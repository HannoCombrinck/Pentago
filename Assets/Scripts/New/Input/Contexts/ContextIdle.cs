using UnityEngine;

public class ContextIdle : InputContext
{
    public override void OnHandleInput()
    {
        if (handler.mousePointer.overClickable)
        {
            handler.SwitchContext(GetComponent<ContextHoverClickable>());
            return;
        }

        if (Input.GetMouseButtonDown(0))
            handler.SwitchContext(GetComponent<ContextOrbitCamera>());

        if (Input.GetMouseButtonDown(1))
            handler.SwitchContext(GetComponent<ContextZoomCamera>());
    }
}
