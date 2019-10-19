using UnityEngine;

public class Match : MonoBehaviour
{
    public IGame game;
    public Board board;
    public IPlayer player1;
    public IPlayer player2;

    public void Begin()
    {
        game.StartNewGame();

        // Notify players that game has started
    }

    public void End()
    {
        // Trigger end of game menu or something

        // Possibly have some kind of "menu cinematic" that starts playing?
    }
}
