
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
        // TODO: Finish implementation
        switch (gameState.currentPlayer)
        {
            case GameState.PLAYER.PLAYER1:
                gameState.boardState[spaceIndex] = GameState.SPACE_STATE.OCCUPIED_PLAYER1;
                break;
            case GameState.PLAYER.PLAYER2:
                gameState.boardState[spaceIndex] = GameState.SPACE_STATE.OCCUPIED_PLAYER2;
                break;
        }
    }
}
