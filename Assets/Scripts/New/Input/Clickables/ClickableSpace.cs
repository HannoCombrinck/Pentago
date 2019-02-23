using UnityEngine;

public class ClickableSpace : MonoBehaviour, IClickable
{
    public GameEvent spaceClicked;
    //public GameEventInfo gameEventInfo;

    private Space space;

    void Awake()
    {
        space = GetComponent<Space>();
        Debug.Assert(space != null);

        //Debug.Assert(spaceClicked != null);
    }

    public void OnLeftClick()
    {
        // TODO: Implement this
        // Player clicked on a space
        //  Validate move
        //      If valid move then execute move MOVE_TYPE.PLACE_MARBLE    
        //      If not valid move then play sound for invalid placement


        // TODO: Where to send this action?
        // new ActionPlaceMarble(space.GetBoardStateIndex());


        //spaceClicked.Raise();
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
