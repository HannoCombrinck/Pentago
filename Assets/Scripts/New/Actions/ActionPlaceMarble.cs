
public class ActionPlaceMarble : IGameAction
{
    public ActionPlaceMarble(int spaceIndex)
    {
        this.spaceIndex = spaceIndex;
    }

    int spaceIndex;

    public string GetDescription()
    {
        return "Marble placed on space " + spaceIndex;
    }

    public void Execute(GameState gameState)
    {
        //TODO: Modify gameState to reflect this change
    }
}
