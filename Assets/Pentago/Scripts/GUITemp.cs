using UnityEngine;

public class GUITemp : MonoBehaviour
{
    public Game game;
    public PentagoNetworkManager networkManager;
    public string player1Name = "Player1 Name";
    public string player2Name = "Player2 Name";
    public PLAYER_TYPE player1Type = PLAYER_TYPE.HUMAN;
    public PLAYER_TYPE player2Type = PLAYER_TYPE.HUMAN;

    private int player1TypeSelection = 0;
    private int player2TypeSelection = 0;
    MENU activeMenu = MENU.MAIN;

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
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();

        //GUIAlways();

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
            activeMenu = MENU.PLAYING;

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

    void GUIPlaying()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Stop Game"))
        {
            activeMenu = MENU.FINISHED;
        }

        GUILayout.EndVertical();
    }

    void GUIFinished()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Back To Main"))
        {
            activeMenu = MENU.MAIN;
        }

        GUILayout.EndVertical();
    }

    private void StartMatch()
    {
        
    }

}
