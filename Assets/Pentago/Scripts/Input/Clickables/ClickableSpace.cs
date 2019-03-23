using UnityEngine;
using static IGame;

// Clicking on a Space attempts to place a marble on that Space.
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
        if (space.state != SPACE_STATE.UNOCCUPIED)
            return;

        board.PlaceMarbleHidePreview();
        board.PlaceMarble(space.spaceIndex);
    }

    public void OnRightClick()
    {
    }

    public void OnMousePointerEnter()
    {
        if (space.state != SPACE_STATE.UNOCCUPIED)
            return;

        if (board.game.GetState().nextMove != MOVE_TYPE.PLACE_MARBLE)
            return;

        board.PlaceMarbleShowPreview(space.spaceIndex);
    }

    public void OnMousePointerExit()
    {
        board.PlaceMarbleHidePreview();
    }
}
