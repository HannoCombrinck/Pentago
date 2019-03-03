
public class ActionRotateQuadrant : IGameAction
{
    public enum DIRECTION
    {
        CLOCKWISE,
        COUNTERCLOCKWISE
    };

    private int quadrantIndex;
    private DIRECTION rotateDirection;

    public ActionRotateQuadrant(int quadrantIndex, DIRECTION rotateDirection)
    {
        this.quadrantIndex = quadrantIndex;
        this.rotateDirection = rotateDirection;
    }

    public string GetDescription()
    {
        return quadrantIndex + " quadrant rotated " + rotateDirection.ToString();
    }

    public void Execute(State gameState)
    {
        var quadrantIndexToBoardIndexMapper = new SpaceIndexMapper(ref gameState.spaceState, quadrantIndex);

        if (rotateDirection == DIRECTION.CLOCKWISE)
            SquareMatrixRotater.Rotate90DegreesClockwise(quadrantIndexToBoardIndexMapper);
        else if (rotateDirection == DIRECTION.COUNTERCLOCKWISE)
            SquareMatrixRotater.Rotate90DegreesCounterclockwise(quadrantIndexToBoardIndexMapper);
    }

    // Maps quadrant space indices (row and column in 3x3 matrix) to board space indices (single index in flat array of size 36 representing 6x6 matrix).
    private class SpaceIndexMapper : ISquareMatrix<CommonTypes.SPACE_STATE>
    {
        private CommonTypes.SPACE_STATE[] spaceState;
        private int quadrantRow;
        private int quadrantCol;

        public SpaceIndexMapper(ref CommonTypes.SPACE_STATE[] spaceState, int quadrantIndex)
        {
            quadrantRow = quadrantIndex / 2;
            quadrantCol = quadrantIndex % 2;
            this.spaceState = spaceState;
        }

        public int GetSize()
        {
            return 3;
        }

        public ref CommonTypes.SPACE_STATE At(int row, int col)
        {
            var boardRow = row + (quadrantRow * GetSize());
            var boardCol = col + (quadrantCol * GetSize());
            return ref spaceState[boardRow * 6 + boardCol];
        }
    }
}