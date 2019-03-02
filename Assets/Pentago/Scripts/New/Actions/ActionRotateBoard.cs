
public class ActionRotateBoard : IGameAction
{
    public enum DIRECTION
    {
        CLOCKWISE,
        COUNTERCLOCKWISE
    };

    private int boardIndex;
    private DIRECTION rotateDirection;

    public ActionRotateBoard(int boardIndex, DIRECTION rotateDirection)
    {
        this.boardIndex = boardIndex;
        this.rotateDirection = rotateDirection;
    }

    public string GetDescription()
    {
        return boardIndex + " board rotated " + rotateDirection.ToString();
    }

    public void Execute(GameState gameState)
    {
        var quadrantToBigBoardMapperMatrix = new SpaceIndexMapper(ref gameState.spaceState, boardIndex);

        if (rotateDirection == DIRECTION.CLOCKWISE)
            SquareMatrixRotater.Rotate90DegreesClockwise(quadrantToBigBoardMapperMatrix);
        else if (rotateDirection == DIRECTION.COUNTERCLOCKWISE)
            SquareMatrixRotater.Rotate90DegreesCounterclockwise(quadrantToBigBoardMapperMatrix);
    }

    // Maps space indices from local 3x3 matrix from quadrant to actual space indices in 6x6 matrix (boardState)
    private class SpaceIndexMapper : ISquareMatrix<CommonTypes.SPACE_STATE>
    {
        private CommonTypes.SPACE_STATE[] spaceState;
        private int boardRow;
        private int boardCol;

        public SpaceIndexMapper(ref CommonTypes.SPACE_STATE[] spaceState, int boardIndex)
        {
            boardRow = boardIndex / 2;
            boardCol = boardIndex % 2;
            this.spaceState = spaceState;
        }

        public int GetSize()
        {
            return 3;
        }

        public ref CommonTypes.SPACE_STATE At(int row, int col)
        {
            var spaceRow = row + (boardRow * GetSize());
            var spaceCol = col + (boardCol * GetSize());
            return ref spaceState[spaceRow * 6 + spaceCol];
        }
    }
}