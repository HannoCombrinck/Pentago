﻿using System;
using UnityEngine;

[RequireComponent(typeof(State))]
public class Game : IGame
{
    private State state;
    private IEvaluator evaluator = new EvaluatorSimple();

    void Awake()
    {
        state = GetComponent<State>();
        state.ResetState();
    }

    public override State GetState()
    {
        return state;
    }

    public override void StartNewGame()
    {
        state.ResetState();
        onNewGameStarted?.Invoke();
    }

    public override void ExitGame()
    {
        Application.Quit();
    }

    public override bool ExecuteAction(IAction action)
    {
        /*if (!action.IsValid(state))
        {
            Debug.Log(state.currentPlayer.ToString() + " attempted an illegal move.");
            onIllegalActionAttempted?.Invoke();
            return false;
        }*/

        action.Execute(state);
        onActionExecuted?.Invoke();

        AdvanceGameState();
        return true;
    }
    
    // Determines the next move, next player and win state and sets the game state to reflect these new values.
    private void AdvanceGameState()
    {
        state.winState = evaluator.Evaluate(state);
        switch (state.winState)
        {
            case WIN_STATE.IN_PROGRESS:
                break;
            case WIN_STATE.TIE:
                HandleTie();
                return;
            case WIN_STATE.PLAYER1_WON:
                HandlePlayerWin();
                return;
            case WIN_STATE.PLAYER2_WON:
                HandlePlayerWin();
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

    private void HandlePlayerWin()
    {
        Array.Copy(evaluator.GetLastEvaluatedLine(), state.winningLine, 5);
        onGameWon?.Invoke();
        //Debug.Log(state.currentPlayer.ToString() + " won the game!");
    }

    private void HandleTie()
    {
        onGameTie?.Invoke();
        Debug.Log("Tied the game!");
    }
}
