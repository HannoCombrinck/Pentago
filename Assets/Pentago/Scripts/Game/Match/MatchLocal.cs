using System.Collections.Generic;
using UnityEngine;

public class MatchLocal : MonoBehaviour, IMatch
{
    public IGame game;
    public Board board;
    public IPlayer player1;
    public IPlayer player2;

    void Awake()
    {

    }

    public void Begin()
    {
        Debug.Assert(player1 != null && player2 != null, "Match requires player1 and player2.");
        Debug.Log("Local match started");

        game.StartNewGame();
    }

    public void End()
    {
        Debug.Log("Local match ended");
        gameObject.SetActive(false);
    }

    public List<IPlayer> GetPlayers()
    {
        return new List<IPlayer>()
        {
            player1,
            player2
        };
    }
}
