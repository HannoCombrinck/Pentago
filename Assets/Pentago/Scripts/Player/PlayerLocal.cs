using UnityEngine;
using static IGame;

public class PlayerLocal : MonoBehaviour, IPlayer
{
    [Tooltip("The board this player is playing on.")]
    public Board board;
    [Tooltip("The player's name.")]
    public string playerName;
    [Tooltip("Player 1 or Player 2.")]
    public PLAYER playerID;

    public string GetName()
    {
        return playerName;
    }

    public PLAYER GetPlayerID()
    {
        return playerID;
    }

    public void ExecutePlaceMarble(int spaceIndex)
    {
        Debug.Assert(board != null, "Board reference required.");

        board.PlaceMarble(this, spaceIndex);
    }

    public void ExecuteRotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        Debug.Assert(board != null, "Board reference required.");

        board.RotateQuadrant(this, quadrantIndex, direction);
    }
}
