using UnityEngine;

public class ClickableSpace : MonoBehaviour, IClickable
{
    public BoardManager boardManager;
    private Space space;

    void Awake()
    {
        space = GetComponent<Space>();
        Debug.Assert(space != null);
    }

    public void OnLeftClick()
    {
        if (space.state == CommonTypes.SPACE_STATE.UNOCCUPIED)
            boardManager.PlaceMarble(space.spaceIndex);
    }

    public void OnRightClick()
    {
        //Debug.Log("Space right clicked " + gameObject.name);
    }

    public void OnMousePointerEnter()
    {
        if (space.state == CommonTypes.SPACE_STATE.UNOCCUPIED)
            boardManager.PlaceMarbleShowPreview(space.spaceIndex);
    }

    public void OnMousePointerExit()
    {
        if (space.state == CommonTypes.SPACE_STATE.UNOCCUPIED)
            boardManager.PlaceMarbleHidePreview();
    }
}
