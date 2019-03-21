
// Interface for any object that can be clicked on with the mouse.
public interface IClickable
{
    // Called when the left mouse button is clicked while pointing at this object.
    void OnLeftClick();
    // Called when the right mouse button is clicked while pointing at this object.
    void OnRightClick();
    // Called once when the mouse pointer moves onto this object.
    void OnMousePointerEnter();
    // Called once when the mouse pointer moves away from this object.
    void OnMousePointerExit();
}
