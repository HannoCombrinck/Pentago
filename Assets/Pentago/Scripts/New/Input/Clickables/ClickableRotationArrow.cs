using UnityEngine;

public class ClickableRotationArrow : MonoBehaviour, IClickable
{
    public Quadrant quadrant;
    public ActionRotateQuadrant.DIRECTION direction;

    void Awake()
    {
        Debug.Assert(quadrant != null);
    }

    public void OnLeftClick()
    {
        quadrant.RotateQuadrant(direction);
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
