using UnityEngine;

public class ClickableRotationArrow : MonoBehaviour, IClickable
{
    public Board board;
    public ActionRotateBoard.DIRECTION direction;

    void Awake()
    {
        Debug.Assert(board != null);
    }

    public void OnLeftClick()
    {
        board.RotateBoard(direction);
    }

    public void OnRightClick()
    {
        Debug.Log("Rotation arrow right clicked " + gameObject.name);
    }

    public void OnMousePointerEnter()
    {
        Debug.Log("Mouse entered Rotation arrow " + gameObject.name);
    }

    public void OnMousePointerExit()
    {
        Debug.Log("Mouse left Rotation arrow " + gameObject.name);
    }
}
