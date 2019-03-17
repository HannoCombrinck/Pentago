
using UnityEngine;
using static IGame;

public class EvaluatorSimple : IEvaluator
{
    private const int winningLineLength = 5;
    private const int boardWidth = 6;
    private const int spaceCount = 36;

    private State gameState;
    private readonly int[] lastLineIndices = new int[winningLineLength];

    private System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    public WIN_STATE Evaluate(State state)
    {
        gameState = state;

        timer.Start();

        int occupiedSpaces = 0;
        for (int row = 0; row < boardWidth; row++)
        {
            for (int col = 0; col < boardWidth; col++)
            {
                var currentSpaceState = gameState.spaces[GetSpaceIndex(row, col)];
                if (currentSpaceState != SPACE_STATE.UNOCCUPIED)
                {
                    occupiedSpaces++;

                    // Check horizontal row
                    if (CountConsecutiveSpaces(row, col, 1, 0) >= winningLineLength)
                        return GetWinState(currentSpaceState);

                    // Check vertical row
                    if (CountConsecutiveSpaces(row, col, 0, 1) >= winningLineLength)
                        return GetWinState(currentSpaceState);

                    // Check first diagonal row
                    if (CountConsecutiveSpaces(row, col, 1, 1) >= winningLineLength)
                        return GetWinState(currentSpaceState);

                    // Check second diagonal row
                    if (CountConsecutiveSpaces(row, col, 1, -1) >= winningLineLength)
                        return GetWinState(currentSpaceState);
                }
            }
        }

        timer.Stop();
        //Debug.Log("EvaluatorSimple took " + timer.ElapsedMilliseconds + "ms to evaluate the current board state.");

        if (occupiedSpaces == spaceCount)
            return WIN_STATE.TIE;

        return WIN_STATE.IN_PROGRESS;
    }

    public int[] GetLastEvaluatedLine()
    {
        return lastLineIndices;
    }

    private int CountConsecutiveSpaces(int fromRow, int fromCol, int rowDirection, int colDirection)
    {
        var lineType = gameState.spaces[GetSpaceIndex(fromRow, fromCol)];

        // Move backward through line to find the first space matching lineType
        var nextRow = fromRow - rowDirection;
        var nextCol = fromCol - colDirection;
        int spaceIndex = GetSpaceIndex(nextRow, nextCol);

        while (IsWithinBounds(nextRow, nextCol) && gameState.spaces[spaceIndex] == lineType)
        {
            fromRow -= rowDirection;
            fromCol -= colDirection;

            nextRow = fromRow - rowDirection;
            nextCol = fromCol - colDirection;
            spaceIndex = GetSpaceIndex(nextRow, nextCol);
        }

        // Move forward through line and count consecutive spaces that match lineType
        int consecutiveCount = 1;
        lastLineIndices[consecutiveCount - 1] = GetSpaceIndex(fromRow, fromCol);
        nextRow = fromRow + rowDirection;
        nextCol = fromCol + colDirection;
        spaceIndex = GetSpaceIndex(nextRow, nextCol);

        while (IsWithinBounds(nextRow, nextCol) && gameState.spaces[spaceIndex] == lineType && consecutiveCount < winningLineLength)
        {
            consecutiveCount++;
            lastLineIndices[consecutiveCount - 1] = spaceIndex;
            fromRow += rowDirection;
            fromCol += colDirection;
            nextRow = fromRow + rowDirection;
            nextCol = fromCol + colDirection;
            spaceIndex = GetSpaceIndex(nextRow, nextCol);
        }

        return consecutiveCount;
    }

    private int GetSpaceIndex(int row, int col)
    {
        return row * boardWidth + col;
    }

    private bool IsWithinBounds(int row, int col)
    {
        if (row < 0 || row >= boardWidth || col < 0 || col >= boardWidth)
            return false;

        return true;
    }

    private WIN_STATE GetWinState(SPACE_STATE spaceState)
    {
        return spaceState == SPACE_STATE.OCCUPIED_PLAYER1 ? WIN_STATE.PLAYER1_WON : WIN_STATE.PLAYER2_WON;
    }
}
