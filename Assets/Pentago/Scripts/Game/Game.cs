using System;
using UnityEngine;

// High level game interface. Manages game state agnostic of graphics/animation etc.
public class Game : MonoBehaviour
{
    public enum PLAYER
    {
        PLAYER1,
        PLAYER2
    }

    public enum WIN_STATE
    {
        IN_PROGRESS,
        PLAYER1_WON,
        PLAYER2_WON
    }

    public enum MOVE_TYPE
    {
        PLACE_MARBLE,
        ROTATE_QUADRANT
    }

    public enum ROTATE_DIRECTION
    {
        CLOCKWISE,
        COUNTERCLOCKWISE
    };

    public enum SPACE_STATE
    {
        UNOCCUPIED,
        OCCUPIED_PLAYER1,
        OCCUPIED_PLAYER2,
    }

    // Reference to the game state instance.
    public State state;

    // Game events
    // {
    public Action onNewGameStarted;
    public Action onGameWon;
    public Action onActionExecuted;
    public Action onGameStateAdvanced;
    public Action onIllegalMove;
    // }

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
            Debug.Log(state.currentPlayer.ToString() + " attempted an illegal move.");
            onIllegalMove?.Invoke();
            return false;
        }

        action.Execute(state);
        onActionExecuted?.Invoke();

        AdvanceGameState();
        return true;
    }
    
    // Determines the next move, next player and win state and sets the game state to reflect these new values.
    private void AdvanceGameState()
    {
        var winState = evaluator.Evaluate(state);
        if (winState != WIN_STATE.IN_PROGRESS)
        {
            state.winState = winState;
            onGameWon?.Invoke();
            Debug.Log(state.currentPlayer.ToString() + " won the game!");

            int indexNumber = 0;
            foreach (int index in evaluator.GetLastEvaluatedLine())
                Debug.Log("Winning line index " + (++indexNumber) + ": " + index);

            return;
        }

        if (state.nextMove == MOVE_TYPE.PLACE_MARBLE)
        {
            state.nextMove = MOVE_TYPE.ROTATE_QUADRANT;
            return;
        }

        state.nextMove = MOVE_TYPE.PLACE_MARBLE;
        state.currentPlayer = state.currentPlayer == PLAYER.PLAYER1 ? PLAYER.PLAYER2 : PLAYER.PLAYER1;

        onGameStateAdvanced?.Invoke();
    }
}
