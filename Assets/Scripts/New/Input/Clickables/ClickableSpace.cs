using UnityEngine;

public class ClickableSpace : MonoBehaviour, IClickable
{
    public GameEvent spaceClicked;

    void Awake()
    {
        //Debug.Assert(spaceClicked != null);
    }

    public void OnLeftClick()
    {
        //spaceClicked.Raise();
        Debug.Log("Space left clicked");
    }

    public void OnRightClick()
    {
        Debug.Log("Space right clicked");
    }

    public void OnMousePointerEnter()
    {
        Debug.Log("Mouse entered space");
        //TODO: preview placement
    }

    public void OnMousePointerExit()
    {
        Debug.Log("Mouse left space");
        //TODO: remove placement preview
    }
}
