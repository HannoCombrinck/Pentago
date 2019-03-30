
using UnityEngine;
using static IGame;

public class ActionPlaceMarble : IAction
{
    public int spaceIndex;

    public ActionPlaceMarble(int spaceIndex)
    {
        this.spaceIndex = spaceIndex;
    }

    public string GetDescription()
    {
        return "Marble placed on space " + spaceIndex;
    }

    public bool IsValid(State gameState)
    {
        if (gameState.nextMove != MOVE_TYPE.PLACE_MARBLE)
            return false;

        if (gameState.spaces[spaceIndex] != SPACE_STATE.UNOCCUPIED)
            return false;

        return true;
    }

    public void Execute(State gameState)
    {
        if (!IsValid(gameState))
        {
            Debug.Log("ActionPlaceMarble: " + gameState.currentPlayer.ToString() + " attempted to illegaly place a marble on space " + spaceIndex);
            return;
        }

        switch (gameState.currentPlayer)
        {
            case PLAYER.PLAYER1:
                gameState.spaces[spaceIndex] = SPACE_STATE.OCCUPIED_PLAYER1;
                break;
            case PLAYER.PLAYER2:
                gameState.spaces[spaceIndex] = SPACE_STATE.OCCUPIED_PLAYER2;
                break;
        }
    }
}
