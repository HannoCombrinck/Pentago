using UnityEngine;

public class GUITemp : MonoBehaviour
{
    public Game game;
    public Board board;
    public PentagoNetworkManager networkManager;
    public PlayerNetworkList networkPlayerList;
    public GameObject match;
    public GameObject playerLocalPrefab;

    public GameObject player1;
    public GameObject player2;
    public string player1Name = "Player1 Name";
    public string player2Name = "Player2 Name";
    public PLAYER_TYPE player1Type = PLAYER_TYPE.HUMAN;
    public PLAYER_TYPE player2Type = PLAYER_TYPE.HUMAN;

    private int player1TypeSelection = 0;
    private int player2TypeSelection = 0;
    MENU activeMenu = MENU.MAIN;

    IMatch currentMatch;

    private bool gameFinished = false;

    enum MENU
    {
        MAIN,
        PREPARE_LOCAL_MATCH,
        PREPARE_NETWORK_MATCH,
        PLAYING,
        FINISHED
    }

    public enum PLAYER_TYPE
    {
        HUMAN,
        AI
    }

    void Awake()
    {
        Debug.Assert(game != null, "Game reference required.");
        Debug.Assert(networkManager != null, "NetworkManager reference required.");
        Debug.Assert(networkPlayerList != null, "NetworkPlayerList reference required.");

        game.onGameTie += OnGameFinished;
        game.onGameWon += OnGameFinished;
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        GUIAlways();

        switch (activeMenu)
        {
            case MENU.MAIN:
                GUIMain();
                break;
            case MENU.PREPARE_LOCAL_MATCH:
                GUIPrepLocalMatch();
                break;
            case MENU.PREPARE_NETWORK_MATCH:
                GUIPrepNetworkMatch();
                break;
            case MENU.PLAYING:
                GUIPlaying();
                break;
            case MENU.FINISHED:
                GUIFinished();
                break;
        }

        GUILayout.EndHorizontal();
    }

    void GUIAlways()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Test Play\nNo players, No network"))
            game.StartNewGame();

        GUILayout.EndVertical();
    }

    void GUIMain()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Local Match"))
        {
            activeMenu = MENU.PREPARE_LOCAL_MATCH;
        }
        if (GUILayout.Button("Network Match"))
        {
            activeMenu = MENU.PREPARE_NETWORK_MATCH;
        }

        GUILayout.EndVertical();
    }

    void GUIPrepLocalMatch()
    {
        GUILayout.BeginVertical();

        player1Name = GUILayout.TextField(player1Name, 50);
        player2Name = GUILayout.TextField(player2Name, 50);

        if (GUILayout.Button("Play"))
        {
            // Setup players based on UI state
            if (player1 != null)
                Destroy(player1);

            var p1 = Instantiate(playerLocalPrefab).GetComponent<PlayerLocal>();
            player1 = p1.gameObject;
            p1.playerID = IGame.PLAYER.PLAYER1;
            p1.playerName = player1Name;
            p1.board = board;
            switch (player1Type) // TODO: Configure player controller (human or AI)
            {
                case PLAYER_TYPE.HUMAN:
                    // TODO
                    break;
                case PLAYER_TYPE.AI:
                    // TODO
                    break;
            }

            if (player2 != null)
                Destroy(player2);

            var p2 = Instantiate(playerLocalPrefab).GetComponent<PlayerLocal>();
            player2 = p2.gameObject;
            p2.playerID = IGame.PLAYER.PLAYER2;
            p2.playerName = player2Name;
            p2.board = board;
            switch (player2Type) // TODO: Configure player controller (human or AI)
            {
                case PLAYER_TYPE.HUMAN:
                    // TODO
                    break;
                case PLAYER_TYPE.AI:
                    // TODO
                    break;
            }

            // Setup match based on players above and UI state about match
            var matchLocal = match.GetComponentInChildren<MatchLocal>(true);
            matchLocal.gameObject.SetActive(true);
            Debug.Assert(matchLocal != null, "MatchLocal component required");
            matchLocal.game = game;
            matchLocal.board = board; // Is this necessary?
            matchLocal.player1 = player1.gameObject.GetComponent<IPlayer>();
            matchLocal.player2 = p2.gameObject.GetComponent<IPlayer>();

            currentMatch = matchLocal;

            // Transition to Playing state
            currentMatch.Begin();
            activeMenu = MENU.PLAYING;
        }

        if (GUILayout.Button("Back"))
            activeMenu = MENU.MAIN;

        GUILayout.EndVertical();

        GUILayout.BeginVertical();

        player1TypeSelection = GUILayout.SelectionGrid(player1TypeSelection, new string[] { "Human", "AI" }, 2);
        player2TypeSelection = GUILayout.SelectionGrid(player2TypeSelection, new string[] { "Human", "AI" }, 2);

        if (player1Type != (PLAYER_TYPE)player1TypeSelection)
            player1Type = (PLAYER_TYPE)player1TypeSelection;
        if (player2Type != (PLAYER_TYPE)player2TypeSelection)
            player2Type = (PLAYER_TYPE)player2TypeSelection;

        GUILayout.EndVertical();
    }

    void GUIPrepNetworkMatch()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Back"))
        {
            activeMenu = MENU.MAIN;
        }

        GUILayout.EndVertical();
    }

    public void OnGameFinished()
    {
        gameFinished = true;
    }

    void GUIPlaying()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Stop Game") || gameFinished)
        {
            Debug.Log("Ending game");

            currentMatch.End();

            Debug.Log("**Showing game result**");
            activeMenu = MENU.FINISHED;
        }

        GUILayout.EndVertical();
    }

    void GUIFinished()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Back To Main"))
        {
            Debug.Log("**No longer showing game result**");

            // TODO: Local players should be destroyed here

            game.StartNewGame(); // TODO: Rather add a StopGame() - otherwise board can be manipulated even though there is no active match

            activeMenu = MENU.MAIN;
        }

        GUILayout.EndVertical();
    }

    private void StartMatch()
    {
        
    }

}
