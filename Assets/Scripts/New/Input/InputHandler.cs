using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public LayerMask clickableLayer;
    public InputContext context;
    public Ray mouseRay;
    public RaycastHit mouseRayInfo;
    public bool mouseOverClickable;
    public bool mouseMoved;

    private InputContext contextToSwitchTo;
    private Vector3 previousMousePosition;

    void Start()
    {
        contextToSwitchTo = context;
        context.Enter(this);
        previousMousePosition = Input.mousePosition;
    }

    void Update()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        mouseOverClickable = Physics.Raycast(mouseRay, out mouseRayInfo, 100f, clickableLayer);

        var mouseDelta = Input.mousePosition - previousMousePosition;
        previousMousePosition = Input.mousePosition;
        mouseMoved = mouseDelta.magnitude > 0.0f;

        context.OnHandleInput();

        if (contextToSwitchTo != context)
        {
            context.OnExit();
            context = contextToSwitchTo;
            context.Enter(this);
        }
    }

    public void SwitchContext(InputContext newContext)
    {
        contextToSwitchTo = newContext;
    }

}
