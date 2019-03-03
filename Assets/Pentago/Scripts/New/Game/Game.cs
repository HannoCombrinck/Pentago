using System;
using UnityEngine;

// High level game interface. Manages game state agnostic of graphics/animation etc.
public class Game : MonoBehaviour
{
    public State state;
    public Settings settings; // TODO: Temp

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

    public void StartNewGame()
    {
        state.ResetState();
        onNewGameStarted?.Invoke();
    }

    public bool ExecuteAction(IAction action)
    {
        if (!IsValidAction(action))
            return false;

        action.Execute(state);
        onActionExecuted?.Invoke();

        if (GetWinState() != CommonTypes.WIN_STATE.IN_PROGRESS)
            onGameWon?.Invoke();

        AdvanceGameState();
        return true;
    }
    
    public void AdvanceGameState()
    {
        if (state.nextMove == CommonTypes.MOVE_TYPE.PLACE_MARBLE)
        {
            state.nextMove = CommonTypes.MOVE_TYPE.ROTATE_QUADRANT;
            return;
        }

        state.nextMove = CommonTypes.MOVE_TYPE.PLACE_MARBLE;
        state.currentPlayer = state.currentPlayer == CommonTypes.PLAYER.PLAYER1 ? CommonTypes.PLAYER.PLAYER2 : CommonTypes.PLAYER.PLAYER1;

        onGameStateAdvanced?.Invoke();
    }

    private bool IsValidAction(IAction action)
    {
        // TODO: Implement this
        return true;
    }

    private CommonTypes.WIN_STATE GetWinState()
    {
        // TODO: Implement this
        return CommonTypes.WIN_STATE.IN_PROGRESS;
    }
}
