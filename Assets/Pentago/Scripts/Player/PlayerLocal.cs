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
    [Tooltip("True if it is this player's turn to play.")]
    public bool turnToPlay = false;

    private void Awake()
    {
        Debug.Assert(board != null, "Board reference required.");
    }

    public string GetName()
    {
        return playerName;
    }

    public PLAYER GetPlayerID()
    {
        return playerID;
    }

    public void PlaceMarble(int spaceIndex)
    {
        board.PlaceMarble(this, spaceIndex);
    }

    public void RotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        board.RotateQuadrant(this, quadrantIndex, direction);
    }

    public void StartTurn()
    {
        turnToPlay = true;
    }

    public void EndTurn()
    {
        turnToPlay = false;
    }
}
