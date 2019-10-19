
// A Clickable is any object that the user can click on with the mouse.
public interface IClickable
{
    // Called when the left mouse button is clicked while pointing at this object.
    void LeftClick(IPlayer player);
    // Called when the right mouse button is clicked while pointing at this object.
    void RightClick(IPlayer player);
    // Called once when the mouse pointer moves onto this object.
    void MousePointerEnter(IPlayer player);
    // Called once when the mouse pointer moves away from this object.
    void MousePointerExit(IPlayer player);
}
