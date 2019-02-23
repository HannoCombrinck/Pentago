
public class ActionRotateBoard : IGameAction
{
    public enum DIRECTION
    {
        CLOCKWISE,
        COUNTERCLOCKWISE
    };

    public ActionRotateBoard(int boardIndex, DIRECTION rotateDirection)
    {
        this.boardIndex = boardIndex;
        this.rotateDirection = rotateDirection;
    }
    
    int boardIndex;
    DIRECTION rotateDirection;

    public string GetDescription()
    {
        return boardIndex + " board rotated " + rotateDirection.ToString();
    }

    public void Execute(GameState gameState)
    {
        //TODO: Modify gameState to reflect this change
    }
}