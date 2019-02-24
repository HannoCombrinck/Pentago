using UnityEngine;

public class Pentago : MonoBehaviour
{
    public GameState state;
    public GameSettings settings;

    void Awake()
    {
        Debug.Assert(state != null);
        state.ResetState();
    }

    public void StartNewGame()
    {
        state.ResetState();
        // TODO: Fire new game event and whatever else needs to happen when a new game starts
    }

    public bool ExecuteAction(IGameAction action)
    {
        // TODO: Check for valid move
        action.Execute(state);
        // TODO: Check win conditions
        AdvanceGameState();
        return true;
    }
    
    public void AdvanceGameState()
    {
        if (state.nextMove == CommonTypes.MOVE_TYPE.PLACE_MARBLE)
        {
            state.nextMove = CommonTypes.MOVE_TYPE.ROTATE_BOARD;
            return;
        }

        state.nextMove = CommonTypes.MOVE_TYPE.PLACE_MARBLE;
        state.currentPlayer = state.currentPlayer == CommonTypes.PLAYER.PLAYER1 ? CommonTypes.PLAYER.PLAYER2 : CommonTypes.PLAYER.PLAYER1;
    }
}
