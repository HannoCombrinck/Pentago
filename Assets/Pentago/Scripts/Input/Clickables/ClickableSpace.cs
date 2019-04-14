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

    public void LeftClick(IPlayer player)
    {
        if (space.state != SPACE_STATE.UNOCCUPIED)
            return;

        player.PlaceMarble(space.spaceIndex);

        //board.PlaceMarbleHidePreview();
        //board.PlaceMarble(space.spaceIndex);
    }

    public void RightClick(IPlayer player)
    {
    }

    public void MousePointerEnter(IPlayer player)
    {
        if (space.state != SPACE_STATE.UNOCCUPIED)
            return;

        if (board.game.GetState().nextMove != MOVE_TYPE.PLACE_MARBLE)
            return;
        
        board.PlaceMarbleShowPreview(player, space.spaceIndex);
    }

    public void MousePointerExit(IPlayer player)
    {
        board.PlaceMarbleHidePreview();
    }
}
