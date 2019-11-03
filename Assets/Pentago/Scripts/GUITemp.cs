using System;
using UnityEngine;
using Mirror;

/// <summary>
/// Temporary quick and dirty menu to test state transitions for setting up single player and multiplayer games. 
/// </summary>
public class GUITemp : MonoBehaviour
{
    public Game game;
    public Board board;
    public NetworkManager networkManager;
    public PlayerNetworkList networkPlayerList;
    public bool readyToStartNetworkMatch = false;
    public Match match;
    public GameObject matchControllers;
    public GameObject matchNetworkPrefab;
    private MatchNetwork matchNetwork = null; 
    public GameObject playerLocalPrefab;

    public GameObject player1;
    public GameObject player2;
    public string player1Name = "Player1 Name";
    public string player2Name = "Player2 Name";
    public string playerLocalName = "PlaerLocal Name";
    public PLAYER_TYPE player1Type = PLAYER_TYPE.HUMAN;
    public PLAYER_TYPE player2Type = PLAYER_TYPE.HUMAN;

    private int player1TypeSelection = 0;
    private int player2TypeSelection = 0;
    MENU activeMenu = MENU.MAIN;

    //IMatch currentMatch;

    private bool gameFinished = false;

    enum MENU
    {
        MAIN,
        PREPARE_LOCAL_MATCH,
        PREPARE_NETWORK_MATCH,
        NETWORK_LOBBY_HOST,
        NETWORK_LOBBY_CLIENT,
        PLAYING_LOCAL_MATCH,
        PLAYING_NETWORK_MATCH,
        FINISHED_LOCAL_MATCH,
        FINISHED_NETWORK_MATCH
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

        game.onGameTie.AddListener(OnGameFinished);
        game.onGameWon.AddListener(OnGameFinished);
        networkPlayerList.onPlayerAdded += OnNetworkPlayerAdded;
        networkPlayerList.onPlayerRemoved += OnNetworkPlayerRemoved;
    }

    private void Update()
    {
        // Temp test game start setup
        if (Input.GetKeyDown(KeyCode.L))
        {
            game.StartNewGame();
            // TODO:
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            game.StartNewGame();
            // TODO:
            //var matchNetwork = match.GetComponentInChildren<MatchNetwork>(true);
            networkManager.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.H))
            networkManager.StartHost();
        if (Input.GetKeyDown(KeyCode.C))
            networkManager.StartClient();
        if (Input.GetKeyDown(KeyCode.S))
            networkManager.StopHost();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        GUIAlways();

        /*switch (activeMenu)
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
            case MENU.NETWORK_LOBBY_HOST:
                GUINetworkLobbyHost();
                break;
            case MENU.NETWORK_LOBBY_CLIENT:
                GUINetworkLobbyClient();
                break;
            case MENU.PLAYING_LOCAL_MATCH:
                GUIPlayingLocal();
                break;
            case MENU.PLAYING_NETWORK_MATCH:
                GUIPlayingNetwork();
                break;
            case MENU.FINISHED_LOCAL_MATCH:
                GUIFinishedLocal();
                break;
            case MENU.FINISHED_NETWORK_MATCH:
                GUIFinishedNetwork();
                break;
        }*/

        GUILayout.EndHorizontal();
    }

    void GUIAlways()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Start Test Game\nNo players, No network"))
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
            var matchNetwork = matchControllers.GetComponentInChildren<MatchNetwork>(true);
            Debug.Assert(matchNetwork != null, "MatchNetword component required");
            //matchNetwork.gameObject.SetActive(true);
            //currentMatch = matchNetwork;
            // Clients can now join network 
            networkManager.gameObject.SetActive(true);

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
            var matchLocal = matchControllers.GetComponentInChildren<MatchLocal>(true);
            matchLocal.gameObject.SetActive(true);
            Debug.Assert(matchLocal != null, "MatchLocal component required");
            matchLocal.match.game = game;
            matchLocal.match.board = board; // Is this necessary?
            matchLocal.match.player1 = player1.gameObject.GetComponent<IPlayer>();
            matchLocal.match.player2 = p2.gameObject.GetComponent<IPlayer>();

            //currentMatch = matchLocal;

            // Transition to Playing state
            //currentMatch.Begin();
            activeMenu = MENU.PLAYING_LOCAL_MATCH;
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

    public void OnNetworkPlayerAdded(PlayerNetwork player)
    {
        if (player1 == null)
            player1 = player.gameObject;
        else if (player2 == null)
            player2 = player.gameObject;

        readyToStartNetworkMatch = player1 != null && player2 != null;
    }

    public void OnNetworkPlayerRemoved(PlayerNetwork player)
    {
        if (player1 == player.gameObject)
            player1 = null;
        else if (player2 == player.gameObject)
            player2 = null;

        readyToStartNetworkMatch = player1 != null && player2 != null;
    }

    void GUIPrepNetworkMatch()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Host Network Game"))
        {
            networkManager.StartHost();
            activeMenu = MENU.NETWORK_LOBBY_HOST;
        }

        if (GUILayout.Button("Join Network Game"))
        {
            networkManager.StartClient();
            activeMenu = MENU.NETWORK_LOBBY_CLIENT;
        }

        if (GUILayout.Button("Back"))
        {
            networkManager.gameObject.SetActive(false);

            activeMenu = MENU.MAIN;
        }

        GUILayout.EndVertical();
    }

    private void GUINetworkLobbyHost()
    {
        GUILayout.BeginVertical();

        // TODO: 
        playerLocalName = GUILayout.TextField(playerLocalName, 50);

        if (networkPlayerList.localPlayer != null)
        {
            if (networkPlayerList.localPlayer.playerName != playerLocalName)
            {
                networkPlayerList.localPlayer.playerName = playerLocalName;
                Debug.Log("Local player name changed to: " + playerLocalName);
            }

            GUILayout.TextArea("Local player: " + networkPlayerList.localPlayer.playerName);
            foreach (var remotePlayer in networkPlayerList.remotePlayers)
            {
                GUILayout.TextArea("Player: " + remotePlayer.playerName);
            }
        }

        if (readyToStartNetworkMatch)
        {
            if (GUILayout.Button("Play"))
            {
                // TODO: Check if at least 2 players has been assigned to game
                Debug.Assert(matchNetwork != null, "matchNetwork should have been spawned already.");

                matchNetwork.match.game = game;
                matchNetwork.match.board = board; // Is this necessary?
                //matchNetwork.player1 = player1;
                //matchNetwork.player2 = player2;
                //currentMatch.Begin();

                activeMenu = MENU.PLAYING_NETWORK_MATCH;
            }
        }

        if (GUILayout.Button("Back"))
        {
            networkManager.StopHost();
            activeMenu = MENU.PREPARE_NETWORK_MATCH;
        }

        GUILayout.EndVertical();
    }

    private void GUINetworkLobbyClient()
    {
        GUILayout.BeginVertical();

        // TODO: 
        playerLocalName = GUILayout.TextField(playerLocalName, 50);

        if (networkPlayerList.localPlayer != null)
        {
            if (networkPlayerList.localPlayer.playerName != playerLocalName)
            {
                networkPlayerList.localPlayer.playerName = playerLocalName;
                Debug.Log("Local player name changed to: " + playerLocalName);
            }

            GUILayout.TextArea("Local player: " + networkPlayerList.localPlayer.playerName);
            foreach (var remotePlayer in networkPlayerList.remotePlayers)
            {
                GUILayout.TextArea("Player: " + remotePlayer.playerName);
            }
        }


        if (GUILayout.Button("Back"))
            activeMenu = MENU.PREPARE_NETWORK_MATCH;

        GUILayout.EndVertical();
    }

    public void OnGameFinished()
    {
        gameFinished = true;
    }

    void GUIPlayingLocal()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Stop Game") || gameFinished)
        {
            Debug.Log("Ending local game");

            //currentMatch.End();

            Debug.Log("**Showing local game result**");
            activeMenu = MENU.FINISHED_LOCAL_MATCH;
        }

        GUILayout.EndVertical();
    }

    void GUIPlayingNetwork()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Stop Game") || gameFinished)
        {
            Debug.Log("Ending network game");

            //currentMatch.End();

            Debug.Log("**Showing network game result**");
            activeMenu = MENU.FINISHED_NETWORK_MATCH;
        }

        GUILayout.EndVertical();
    }

    void GUIFinishedLocal()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Back To Main"))
        {
            Debug.Log("**No longer showing local game result**");

            // TODO: Local players should be destroyed here

            // TODO: Is other network cleanup required here?
            networkManager.gameObject.SetActive(false);

            game.StartNewGame(); // TODO: Rather add a StopGame() - otherwise board can be manipulated even though there is no active match

            activeMenu = MENU.MAIN;
        }

        GUILayout.EndVertical();
    }

    void GUIFinishedNetwork()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Back To Main"))
        {
            networkManager.StopHost();

            Debug.Log("**No longer showing network game result**");

            // TODO: Local players should be destroyed here

            // TODO: Is other network cleanup required here?
            networkManager.gameObject.SetActive(false);

            game.StartNewGame(); // TODO: Rather add a StopGame() - otherwise board can be manipulated even though there is no active match

            activeMenu = MENU.MAIN;
        }

        GUILayout.EndVertical();
    }

}
