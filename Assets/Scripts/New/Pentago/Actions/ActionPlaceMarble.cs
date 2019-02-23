
public class ActionPlaceMarble : IGameAction
{
    ActionPlaceMarble(GameState.PLAYER player, int spaceIndex)
    {
        this.player = player;
        this.spaceIndex = spaceIndex;
    }

    GameState.PLAYER player = GameState.PLAYER.PLAYER1;
    int spaceIndex;

    public string GetDescription()
    {
        return player.ToString() + " placed a marble on space " + spaceIndex;
    }

    public void Execute(GameState gameState)
    {
        //TODO: Modify gameState to reflect this change
    }
}
