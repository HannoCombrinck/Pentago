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

    public override bool ExecuteAction(IAction action)
    {
        if (!action.IsValid(state))
        {
            Debug.Log(state.currentPlayer.ToString() + " attempted an illegal move.");
            onIllegalActionAttempted?.Invoke();
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
