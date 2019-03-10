
using UnityEngine;

public class EvaluatorSimple : IEvaluator
{
    private const int winningRowLength = 5;
    private const int boardWidth = 6;
    private const int boardIndexMax = 35;

    private State gameState;
    private readonly int[] winningRowIndices = new int[5];

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
                    var currentPlayerWin = currentSpaceState == CommonTypes.SPACE_STATE.OCCUPIED_PLAYER1 ? CommonTypes.WIN_STATE.PLAYER1_WON : CommonTypes.WIN_STATE.PLAYER2_WON;

                    // Check horizontal row
                    if (CountSpaces(currentSpaceState, 1, 0, row, col) >= winningRowLength)
                        return currentPlayerWin;

                    // Check vertical row
                    if (CountSpaces(currentSpaceState, 0, 1, row, col) >= winningRowLength)
                        return currentPlayerWin;

                    // Check first diagonal row
                    if (CountSpaces(currentSpaceState, 1, 1, row, col) >= winningRowLength)
                        return currentPlayerWin;

                    // Check second diagonal row
                    if (CountSpaces(currentSpaceState, 1, -1, row, col) >= winningRowLength)
                        return currentPlayerWin;
                }
            }
        }

        timer.Stop();
        Debug.Log("EvaluatorSimple took " + timer.ElapsedMilliseconds + "ms to evaluate the current board state.");

        return CommonTypes.WIN_STATE.IN_PROGRESS;
    }

    public int[] GetWinningRow()
    {
        return winningRowIndices;
    }

    private int CountSpaces(CommonTypes.SPACE_STATE spaceState, int rowDelta, int colDelta, int row, int col)
    {
        int spaceIndex = GetSpaceIndex(row - rowDelta, col - colDelta);

        while (IsValidIndex(spaceIndex) && gameState.spaces[spaceIndex] == spaceState)
        {
            row -= rowDelta;
            col -= colDelta;
            spaceIndex = GetSpaceIndex(row - rowDelta, col - colDelta);
        }

        int count = 0;
        _CountSpaces(spaceState, rowDelta, colDelta, row, col, ref count);
        return count;
    }

    private void _CountSpaces(CommonTypes.SPACE_STATE spaceState, int rowDelta, int colDelta, int row, int col, ref int count)
    {
        int spaceIndex = GetSpaceIndex(row, col);
        if (!IsValidIndex(spaceIndex))
            return;

        winningRowIndices[count++] = spaceIndex;

        if (count > winningRowLength)
            return;

        if (gameState.spaces[spaceIndex] == spaceState)
            _CountSpaces(spaceState, rowDelta, colDelta, row + rowDelta, col + colDelta, ref count);

        return;
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
}
