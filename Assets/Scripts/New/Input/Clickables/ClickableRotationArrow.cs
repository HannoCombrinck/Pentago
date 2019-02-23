using UnityEngine;

public class ClickableRotationArrow : MonoBehaviour, IClickable
{
    public void OnLeftClick()
    {
        Debug.Log("Rotation arrow left clicked");
    }

    public void OnRightClick()
    {
        Debug.Log("Rotation arrow right clicked");
    }

    public void OnMousePointerEnter()
    {
        Debug.Log("Mouse entered Rotation arrow");
    }

    public void OnMousePointerExit()
    {
        Debug.Log("Mouse left Rotation arrow");
    }
}
