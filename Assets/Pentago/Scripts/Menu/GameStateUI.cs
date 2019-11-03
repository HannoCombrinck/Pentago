using UnityEngine;

public class GameStateUI : MonoBehaviour
{
    public Game Game;
    public GameObject Player1Wins;
    public GameObject Player2Wins;
    public GameObject Draw;

    void Awake()
    {
        Debug.Assert(Game != null);
        Debug.Assert(Player1Wins != null);
        Debug.Assert(Player2Wins != null);
        Debug.Assert(Draw != null);

        Game.onNewGameStarted.AddListener(UpdateUI);
        Game.onGameWon.AddListener(UpdateUI);
        Game.onGameTie.AddListener(UpdateUI);
    }

    void UpdateUI()
    {
        switch (Game.GetState().winState)
        {
            case IGame.WIN_STATE.IN_PROGRESS:
                Player1Wins.SetActive(false);
                Player2Wins.SetActive(false);
                Draw.SetActive(false);
                break;
            case IGame.WIN_STATE.TIE:
                Player1Wins.SetActive(false);
                Player2Wins.SetActive(false);
                Draw.SetActive(true);
                break;
            case IGame.WIN_STATE.PLAYER1_WON:
                Player1Wins.SetActive(true);
                Player2Wins.SetActive(false);
                Draw.SetActive(false);
                break;
            case IGame.WIN_STATE.PLAYER2_WON:
                Player1Wins.SetActive(false);
                Player2Wins.SetActive(true);
                Draw.SetActive(false);
                break;
        }
    }
}
