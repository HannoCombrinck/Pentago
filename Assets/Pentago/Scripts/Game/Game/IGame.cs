using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class IGame : MonoBehaviour
{
    public enum PLAYER
    {
        PLAYER1,
        PLAYER2
    }

    public enum WIN_STATE
    {
        IN_PROGRESS,
        TIE,
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

    public const int winningLineLength = 5;
    public const int boardWidth = 6;
    public const int spaceCount = 36;

    // Game events.
    // {
    public UnityEvent onNewGameStarted;
    public UnityEvent onGameWon;
    public UnityEvent onGameTie;
    public UnityEvent onActionExecuted;
    public UnityEvent onGameStateAdvanced;
    //public Action onIllegalActionAttempted;
    // }

    // Get the current game state.
    public abstract State GetState();

    // Set game state to represent the beginning of a new game.
    public abstract void StartNewGame();

    // Exit the application
    public abstract void ExitGame();

    // Execute the given action by updating the game state accordingly.
    // Return false if the action is invalid/illegal without executing it.
    // Return true if the action is valid and was executed on the game state.
    public abstract bool ExecuteAction(IAction action);
}
