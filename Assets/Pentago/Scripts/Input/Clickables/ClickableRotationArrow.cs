using UnityEngine;
using static IGame;

// Clicking on a rotation arrow attempts to rotate the Quadrant it is connected to.
public class ClickableRotationArrow : MonoBehaviour, IClickable
{
    public Board board;
    public Quadrant quadrant;
    public ROTATE_DIRECTION direction;

    void Awake()
    {
        Debug.Assert(quadrant != null);
    }

    public void LeftClick(IPlayer player)
    {
        player.RotateQuadrant(quadrant.quadrantIndex, direction);

        //board.RotateQuadrant(quadrant.quadrantIndex, direction);
    }

    public void RightClick(IPlayer player)
    {
    }

    public void MousePointerEnter(IPlayer player)
    {
    }

    public void MousePointerExit(IPlayer player)
    {
    }
}
