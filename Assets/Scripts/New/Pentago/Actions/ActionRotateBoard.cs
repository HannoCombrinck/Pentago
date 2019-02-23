
public class ActionRotateBoard : IGameAction
{
    public enum DIRECTION
    {
        CLOCKWISE,
        COUNTERCLOCKWISE
    };

    ActionRotateBoard(GameState.PLAYER player, int boardIndex, DIRECTION rotateDirection)
    {
        this.player = player;
        this.boardIndex = boardIndex;
        this.rotateDirection = rotateDirection;
    }
    
    GameState.PLAYER player = GameState.PLAYER.PLAYER1;
    int boardIndex;
    DIRECTION rotateDirection;

    public string GetDescription()
    {
        return player.ToString() + " rotated board " + boardIndex + " " + rotateDirection.ToString();
    }

    public void Execute(GameState gameState)
    {
        //TODO: Modify gameState to reflect this change
    }
}