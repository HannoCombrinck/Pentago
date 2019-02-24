using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameState gameState;

    void Awake()
    {
        Debug.Assert(gameState != null);
        gameState.ResetState();
    }

    public void StartNewGame()
    {
        gameState.ResetState();
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
