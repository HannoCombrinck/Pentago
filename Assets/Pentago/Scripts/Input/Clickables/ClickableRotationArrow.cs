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

    public void OnLeftClick()
    {
        board.RotateQuadrant(quadrant.quadrantIndex, direction);
    }

    public void OnRightClick()
    {
    }

    public void OnMousePointerEnter()
    {
    }

    public void OnMousePointerExit()
    {
    }
}
