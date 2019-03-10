using UnityEngine;

public class ClickableSpace : MonoBehaviour, IClickable
{
    public Board board;
    private Space space;

    void Awake()
    {
        space = GetComponent<Space>();
        Debug.Assert(space != null);
    }

    public void OnLeftClick()
    {
        if (space.state == CommonTypes.SPACE_STATE.UNOCCUPIED)
            board.PlaceMarble(space.spaceIndex);
    }

    public void OnRightClick()
    {
    }

    public void OnMousePointerEnter()
    {
        if (space.state == CommonTypes.SPACE_STATE.UNOCCUPIED)
            board.PlaceMarbleShowPreview(space.spaceIndex);
    }

    public void OnMousePointerExit()
    {
        if (space.state == CommonTypes.SPACE_STATE.UNOCCUPIED)
            board.PlaceMarbleHidePreview();
    }
}
