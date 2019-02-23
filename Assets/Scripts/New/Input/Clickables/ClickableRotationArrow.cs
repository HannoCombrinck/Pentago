﻿using UnityEngine;

public class ClickableRotationArrow : MonoBehaviour, IClickable
{
    public void OnLeftClick()
    {
        Debug.Log("Rotation arrow left clicked " + gameObject.name);
    }

    public void OnRightClick()
    {
        Debug.Log("Rotation arrow right clicked " + gameObject.name);
    }

    public void OnMousePointerEnter()
    {
        Debug.Log("Mouse entered Rotation arrow " + gameObject.name);
    }

    public void OnMousePointerExit()
    {
        Debug.Log("Mouse left Rotation arrow " + gameObject.name);
    }
}
