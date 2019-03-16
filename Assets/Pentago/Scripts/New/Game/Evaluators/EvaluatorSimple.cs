
using UnityEngine;

public class EvaluatorSimple : IEvaluator
{
    private const int winningLineLength = 5;
    private const int boardWidth = 6;
    private const int boardIndexMax = 35;

    private State gameState;
    private readonly int[] lastLineIndices = new int[5];

    private System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    public CommonTypes.WIN_STATE Evaluate(State gameState)
    {
        this.gameState = gameState;

        timer.Start();

        for (int row = 0; row < boardWidth; row++)
        {
            for (int col = 0; col < boardWidth; col++)
            {
                var currentSpaceState = this.gameState.spaces[GetSpaceIndex(row, col)];
                if (currentSpaceState != CommonTypes.SPACE_STATE.UNOCCUPIED)
                {
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
        Debug.Log("EvaluatorSimple took " + timer.ElapsedMilliseconds + "ms to evaluate the current board state.");

        return CommonTypes.WIN_STATE.IN_PROGRESS;
    }

    public int[] GetLastEvaluatedLine()
    {
        return lastLineIndices;
    }

    private int CountConsecutiveSpaces(int fromRow, int fromCol, int rowDirection, int colDirection)
    {
        var lineType = gameState.spaces[GetSpaceIndex(fromRow, fromCol)];

        // Move backward through line to find the first space matching lineType
        int spaceIndex = GetSpaceIndex(fromRow - rowDirection, fromCol - colDirection);
        while (IsValidIndex(spaceIndex) && gameState.spaces[spaceIndex] == lineType)
        {
            fromRow -= rowDirection;
            fromCol -= colDirection;
            spaceIndex = GetSpaceIndex(fromRow - rowDirection, fromCol - colDirection);
        }

        // Move forward through line and count consecutive spaces that match lineType
        int consecutiveCount = 1;
        lastLineIndices[consecutiveCount - 1] = GetSpaceIndex(fromRow, fromCol);
        spaceIndex = GetSpaceIndex(fromRow + rowDirection, fromCol + colDirection);
        while (IsValidIndex(spaceIndex) && gameState.spaces[spaceIndex] == lineType && consecutiveCount < 5)
        {
            consecutiveCount++;
            lastLineIndices[consecutiveCount - 1] = spaceIndex;
            fromRow += rowDirection;
            fromCol += colDirection;
            spaceIndex = GetSpaceIndex(fromRow + rowDirection, fromCol + colDirection);
        }

        return consecutiveCount;
    }

    private int GetSpaceIndex(int row, int col)
    {
        return row * boardWidth + col;
    }

    private bool IsValidIndex(int spaceIndex)
    {
        if (spaceIndex < 0 || spaceIndex > boardIndexMax)
            return false;
        return true;
    }

    private CommonTypes.WIN_STATE GetWinState(CommonTypes.SPACE_STATE spaceState)
    {
        return spaceState == CommonTypes.SPACE_STATE.OCCUPIED_PLAYER1 ? CommonTypes.WIN_STATE.PLAYER1_WON : CommonTypes.WIN_STATE.PLAYER2_WON;
    }

}
