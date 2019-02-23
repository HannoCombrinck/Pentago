using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameState gameState;

    void Awake()
    {
        Debug.Assert(gameState != null);
        ResetGameState();
    }

    public void ResetGameState()
    {
        // TODO: Set initial gameState
        gameState.currentPlayer = GameState.PLAYER.PLAYER1;
        gameState.winState = GameState.WIN_STATE.IN_PROGRESS;
        gameState.nextMove = GameState.MOVE_TYPE.PLACE_MARBLE;

        for (int i = 0; i < gameState.boardState.Length; i++)
            gameState.boardState[i] = GameState.SPACE_STATE.UNOCCUPIED;
    }

    public void StartNewGame()
    {
        ResetGameState();
        // TODO: Fire new game event and whatever else needs to happen when a new game starts
    }

    public bool ExecuteGameAction(IGameAction action)
    {
        // TODO: Check for valid move
        action.Execute(gameState);
        // TODO: Check win conditions
        AdvanceGameState();
        return true;
    }
    
    public void AdvanceGameState()
    {
        if (gameState.nextMove == GameState.MOVE_TYPE.PLACE_MARBLE)
        {
            gameState.nextMove = GameState.MOVE_TYPE.ROTATE_BOARD;
            return;
        }

        gameState.nextMove = GameState.MOVE_TYPE.PLACE_MARBLE;
        gameState.currentPlayer = gameState.currentPlayer == GameState.PLAYER.PLAYER1 ? GameState.PLAYER.PLAYER2 : GameState.PLAYER.PLAYER1;
    }
}
