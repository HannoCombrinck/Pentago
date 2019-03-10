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
    public Action onIllegalMove;
    #endregion

    private IEvaluator evaluator = new EvaluatorSimple();

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
        if (!action.IsValid(state))
        {
            Debug.LogError("This should never happen.");
            return false;
        }

        action.Execute(state);
        onActionExecuted?.Invoke();

        AdvanceGameState();
        return true;
    }
    
    // Determines the next move, next player and win state and sets the game state to reflect these new values.
    public void AdvanceGameState()
    {
        var winState = evaluator.Evaluate(state);
        if (winState != CommonTypes.WIN_STATE.IN_PROGRESS)
        {
            state.winState = winState;
            onGameWon?.Invoke();
            Debug.Log(state.currentPlayer.ToString() + " won the game!");

            foreach (int i in evaluator.GetWinningRow())
                Debug.Log("Winning index: " + i);

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

    // Checks if the given action is a valid action to apply to the current state of the game.
    public bool IsValidAction(IAction action)
    {
        if (!action.IsValid(state))
        {
            Debug.Log(state.currentPlayer.ToString() + " attempted an illegal move.");
            onIllegalMove?.Invoke();
            return false;
        }
        return true;
    }
}
