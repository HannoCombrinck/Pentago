
using UnityEngine;

public class ActionPlaceMarble : IAction
{
    public ActionPlaceMarble(int spaceIndex)
    {
        this.spaceIndex = spaceIndex;
    }

    public int spaceIndex;

    public string GetDescription()
    {
        return "Marble placed on space " + spaceIndex;
    }

    public bool IsValid(State gameState)
    {
        if (gameState.nextMove != CommonTypes.MOVE_TYPE.PLACE_MARBLE)
            return false;

        if (gameState.spaces[spaceIndex] != CommonTypes.SPACE_STATE.UNOCCUPIED)
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
            case CommonTypes.PLAYER.PLAYER1:
                gameState.spaces[spaceIndex] = CommonTypes.SPACE_STATE.OCCUPIED_PLAYER1;
                break;
            case CommonTypes.PLAYER.PLAYER2:
                gameState.spaces[spaceIndex] = CommonTypes.SPACE_STATE.OCCUPIED_PLAYER2;
                break;
        }
    }
}
