using System;
using UnityEngine;

// High level game interface. Manages game state agnostic of graphics/animation etc.
public class Game : MonoBehaviour
{
    // Reference to the game state instance.
    public State state;

    #region Game events
    public Action onNewGameStarted;
    public Action onGameWon;
    public Action onActionExecuted;
    public Action onGameStateAdvanced;
    #endregion

    void Awake()
    {
        Debug.Assert(state != null, "Game state reference is required.");
        state.ResetState();
    }

    // Start a new game by resetting the game state.
    public void StartNewGame()
    {
        state.ResetState();
        onNewGameStarted?.Invoke();
    }

    // If the given action is valid then execute it and advance the game state.
    public bool ExecuteAction(IAction action)
    {
        if (!IsValidAction(action))
            return false;

        action.Execute(state);
        onActionExecuted?.Invoke();

        AdvanceGameState();
        return true;
    }
    
    // Determines the next move, next player and win state and sets the game state to reflect these new values.
    public void AdvanceGameState()
    {
        var winState = CheckBoardState();
        if (winState != CommonTypes.WIN_STATE.IN_PROGRESS)
        {
            state.winState = winState;
            onGameWon?.Invoke();
            return;
        }

        if (state.nextMove == CommonTypes.MOVE_TYPE.PLACE_MARBLE)
        {
            state.nextMove = CommonTypes.MOVE_TYPE.ROTATE_QUADRANT;
            return;
        }

        state.nextMove = CommonTypes.MOVE_TYPE.PLACE_MARBLE;
        state.currentPlayer = state.currentPlayer == CommonTypes.PLAYER.PLAYER1 ? CommonTypes.PLAYER.PLAYER2 : CommonTypes.PLAYER.PLAYER1;

        onGameStateAdvanced?.Invoke();
    }

    // Checks if action is a valid move in the current game state.
    public bool IsValidAction(IAction action)
    {
        // TODO: Implement this
        return true;
    }

    // Check the current state of the board and return the appropriate WIN_STATE.
    private CommonTypes.WIN_STATE CheckBoardState()
    {
        // TODO: Implement this
        return CommonTypes.WIN_STATE.IN_PROGRESS;
    }
}
