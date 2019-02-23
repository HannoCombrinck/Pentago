using UnityEngine;

public class ClickableSpace : MonoBehaviour, IClickable
{
    public GameState gameState;
    public GameEvent spaceClicked;

    public enum STATE
    {
        UNOCCUPIED,
        OCCUPIED_WHITE,
        OCCUPIED_BLACK
    }
    public STATE currentState;

    void Awake()
    {
        //Debug.Assert(spaceClicked != null);
    }

    public void OnLeftClick()
    {
        //spaceClicked.Raise();
        Debug.Log("Space left clicked");
        Debug.Log("Player " + gameState.currentPlayer + " clicked space " + gameObject.name);
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
