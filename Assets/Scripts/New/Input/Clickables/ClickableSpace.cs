using UnityEngine;

public class ClickableSpace : MonoBehaviour, IClickable
{
    public Manager gameManager;

    private Space space;

    void Awake()
    {
        space = GetComponent<Space>();
        Debug.Assert(space != null);

        //Debug.Assert(spaceClicked != null);
    }

    public void OnLeftClick()
    {
        // TODO: ???
        gameManager.ExecuteAction(new ActionPlaceMarble(space.GetBoardStateIndex()));
        //

        Debug.Log("Player " + space.gameState.currentPlayer.ToString() + " clicked space " + gameObject.name + ", space state: " + space.state.ToString());
    }

    public void OnRightClick()
    {
        Debug.Log("Space right clicked " + gameObject.name);
    }

    public void OnMousePointerEnter()
    {
        Debug.Log("Mouse entered space " + gameObject.name);
        //TODO: preview placement
    }

    public void OnMousePointerExit()
    {
        Debug.Log("Mouse left space " + gameObject.name);
        //TODO: remove placement preview
    }
}
