
// A Clickable is any object that the user can click on with the mouse.
public interface IClickable
{
    // Called when the left mouse button is clicked while pointing at this object.
    void LeftClick();
    // Called when the right mouse button is clicked while pointing at this object.
    void RightClick();
    // Called once when the mouse pointer moves onto this object.
    void MousePointerEnter();
    // Called once when the mouse pointer moves away from this object.
    void MousePointerExit();
}
