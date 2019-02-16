﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextHoverClickable : InputContext
{
    private InputContext contextIdle;
    private GameObject clickable;

    void Awake()
    {
        contextIdle = GetComponent<ContextIdle>();
    }

    public override void OnEnter()
    {
        SetClickable(handler.mouseRayInfo.collider.gameObject);
        Debug.Log("Entering hover clickable context: " + clickable.name);
    }

    public override void OnExit()
    {
        Debug.Log("Exiting hover clickable context");
    }

    public override void OnHandleInput()
    {
        if (!handler.mouseOverClickable)
        {
            handler.SwitchContext(contextIdle);
            return;
        }
        else if (clickable != handler.mouseRayInfo.collider.gameObject)
        {
            SetClickable(handler.mouseRayInfo.collider.gameObject);
        }


    }

    private void SetClickable(GameObject newClickable)
    {
        DeselectClickable();
        clickable = newClickable;
    }

    private void DeselectClickable()
    {
        clickable = null;
    }
}
