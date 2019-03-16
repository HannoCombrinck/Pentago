using UnityEngine;
using static CommonTypes;

public class ClickableRotationArrow : MonoBehaviour, IClickable
{
    public Board boardManager;
    public Quadrant quadrant;
    public ROTATE_DIRECTION direction;

    void Awake()
    {
        Debug.Assert(quadrant != null);
    }

    public void OnLeftClick()
    {
        boardManager.RotateQuadrant(quadrant.quadrantIndex, direction);
    }

    public void OnRightClick()
    {
        //Debug.Log("Rotation arrow right clicked " + gameObject.name);
    }

    public void OnMousePointerEnter()
    {
        //Debug.Log("Mouse entered Rotation arrow " + gameObject.name);
    }

    public void OnMousePointerExit()
    {
        //Debug.Log("Mouse left Rotation arrow " + gameObject.name);
    }
}
