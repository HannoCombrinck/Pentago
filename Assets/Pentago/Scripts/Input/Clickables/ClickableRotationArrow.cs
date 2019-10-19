using UnityEngine;
using static IGame;

// Clicking on a rotation arrow attempts to rotate the Quadrant it is connected to.
public class ClickableRotationArrow : MonoBehaviour, IClickable
{
    public delegate void InputEvent(int quadrantIndex, ROTATE_DIRECTION arrowDirection);
    public static InputEvent onClick;
    public static InputEvent onMouseEnter;
    public static InputEvent onMouseExit;

    public Board board;
    public Quadrant quadrant;
    public ROTATE_DIRECTION direction;

    void Awake()
    {
        Debug.Assert(quadrant != null);
    }

    public void LeftClick(IPlayer player)
    {
        onClick?.Invoke(quadrant.quadrantIndex, direction);

        // TODO: Remove this
        board.RotateQuadrant(null, quadrant.quadrantIndex, direction);
        //
    }

    public void RightClick(IPlayer player)
    {
    }

    public void MousePointerEnter(IPlayer player)
    {
        onMouseEnter?.Invoke(quadrant.quadrantIndex, direction);
    }

    public void MousePointerExit(IPlayer player)
    {
        onMouseEnter?.Invoke(quadrant.quadrantIndex, direction);
    }
}
