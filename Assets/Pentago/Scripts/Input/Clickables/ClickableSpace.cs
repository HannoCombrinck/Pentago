using UnityEngine;
using static IGame;

// Clicking on a Space attempts to place a marble on that Space.
public class ClickableSpace : MonoBehaviour, IClickable
{
    public delegate void InputEvent(int spaceIndex);
    public static InputEvent onClick;
    public static InputEvent onMouseEnter;
    public static InputEvent onMouseExit;

    public Board board; // TEMP 

    private Space space;

    void Awake()
    {
        space = GetComponent<Space>();
        Debug.Assert(space != null);
    }

    public void LeftClick(IPlayer player)
    {
        if (space.state != SPACE_STATE.UNOCCUPIED)
            return;

        onClick?.Invoke(space.spaceIndex);

        // TODO: Remove this
        board.PlaceMarbleHidePreview();
        board.PlaceMarble(null, space.spaceIndex);
        //
    }

    public void RightClick(IPlayer player)
    {
    }

    public void MousePointerEnter(IPlayer player)
    {
        if (space.state != SPACE_STATE.UNOCCUPIED)
            return;

        onMouseEnter?.Invoke(space.spaceIndex);

        // TODO: Remove this
        if (board.game.GetState().nextMove != MOVE_TYPE.PLACE_MARBLE)
            return;
        
        board.PlaceMarbleShowPreview(space.spaceIndex);
    }

    public void MousePointerExit(IPlayer player)
    {
        onMouseExit?.Invoke(space.spaceIndex);

        board.PlaceMarbleHidePreview();
    }
}
