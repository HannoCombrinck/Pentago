
public class ActionPlaceMarble : IGameAction
{
    public ActionPlaceMarble(int spaceIndex)
    {
        this.spaceIndex = spaceIndex;
    }

    public int spaceIndex;

    public string GetDescription()
    {
        return "Marble placed on space " + spaceIndex;
    }

    public void Execute(State gameState)
    {
        // TODO: Finish implementation
        switch (gameState.currentPlayer)
        {
            case CommonTypes.PLAYER.PLAYER1:
                gameState.spaceState[spaceIndex] = CommonTypes.SPACE_STATE.OCCUPIED_PLAYER1;
                break;
            case CommonTypes.PLAYER.PLAYER2:
                gameState.spaceState[spaceIndex] = CommonTypes.SPACE_STATE.OCCUPIED_PLAYER2;
                break;
        }
    }
}
