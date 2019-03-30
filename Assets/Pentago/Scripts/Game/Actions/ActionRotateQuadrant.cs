
using UnityEngine;
using static IGame;

public class ActionRotateQuadrant : IAction
{
    private int quadrantIndex;
    private ROTATE_DIRECTION rotateDirection;

    private QuadrantMatrix quadrantMatrix = new QuadrantMatrix();

    public ActionRotateQuadrant(int quadrantIndex, ROTATE_DIRECTION rotateDirection)
    {
        this.quadrantIndex = quadrantIndex;
        this.rotateDirection = rotateDirection;
    }

    public string GetDescription()
    {
        return "Quadrant " + quadrantIndex + " rotated " + rotateDirection.ToString();
    }

    public bool IsValid(State gameState)
    {
        if (gameState.nextMove != MOVE_TYPE.ROTATE_QUADRANT)
            return false;

        return true;
    }

    public void Execute(State gameState)
    {
        if (!IsValid(gameState))
        {
            Debug.Log("ActionRotateQuadrant: " + gameState.currentPlayer.ToString() + " attempted to illegaly rotate quadrant " + quadrantIndex + " " + rotateDirection.ToString());
            return;
        }
        
        quadrantMatrix.SetQuadrant(ref gameState.spaces, quadrantIndex);

        if (rotateDirection == ROTATE_DIRECTION.CLOCKWISE)
            SquareMatrixRotater.Rotate90DegreesClockwise(quadrantMatrix);
        else if (rotateDirection == ROTATE_DIRECTION.COUNTERCLOCKWISE)
            SquareMatrixRotater.Rotate90DegreesCounterclockwise(quadrantMatrix);
    }

    // Maps quadrant space indices (row and column in 3x3 matrix) to board space indices (single index in flat array of size 36 representing 6x6 matrix).
    private class QuadrantMatrix : ISquareMatrix<SPACE_STATE>
    {
        private SPACE_STATE[] spaceState;
        private int quadrantRow;
        private int quadrantCol;

        public void SetQuadrant(ref SPACE_STATE[] spaceState, int quadrantIndex)
        {
            quadrantRow = quadrantIndex / 2;
            quadrantCol = quadrantIndex % 2;
            this.spaceState = spaceState;
        }

        public int GetSize()
        {
            return 3;
        }

        public ref SPACE_STATE At(int row, int col)
        {
            var boardRow = row + (quadrantRow * GetSize());
            var boardCol = col + (quadrantCol * GetSize());
            return ref spaceState[boardRow * 6 + boardCol];
        }
    }
}
