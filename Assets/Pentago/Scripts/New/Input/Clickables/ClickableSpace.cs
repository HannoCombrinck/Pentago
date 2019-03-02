using UnityEngine;

public class ClickableSpace : MonoBehaviour, IClickable
{
    private Space space;

    void Awake()
    {
        space = GetComponent<Space>();
        Debug.Assert(space != null);
    }

    public void OnLeftClick()
    {
        // TODO: Change this to call Space.PlaceMarble() as opposed to calling game.ExecuteAction directly
        space.PlaceMarble();

        Debug.Log("Player " + space.game.state.currentPlayer.ToString() + " clicked space " + gameObject.name + ", space state: " + space.state.ToString());
    }

    public void OnRightClick()
    {
        //Debug.Log("Space right clicked " + gameObject.name);
    }

    public void OnMousePointerEnter()
    {
        //Debug.Log("Mouse entered space " + gameObject.name);
        //TODO: preview placement
    }

    public void OnMousePointerExit()
    {
        //Debug.Log("Mouse left space " + gameObject.name);
        //TODO: remove placement preview
    }
}
