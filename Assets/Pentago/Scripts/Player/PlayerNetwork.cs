using UnityEngine;
using UnityEngine.Networking;
using static IGame;

#pragma warning disable CS0618 // Type or member is obsolete
public class PlayerNetwork : NetworkBehaviour, IPlayer
#pragma warning restore CS0618 // Type or member is obsolete
{
    [Tooltip("The board this player is playing on.")]
    public Board board;
    [Tooltip("The player's name.")]
    public string playerName;
    [Tooltip("Player 1 or Player 2.")]
    public PLAYER playerID;
    [Tooltip("Asset used to keep track of players on the network.")]
    public PlayerNetworkList playerList = null;

    private void Awake()
    {
        Debug.Assert(board != null, "Board reference required.");
        Debug.Assert(playerList != null, "NetworkPlayerList reference required.");
        Debug.Log("PlayerNetwork Awake()");
    }

    private void Start()
    {
        playerList.AddPlayer(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Player: OnStartClient: " + playerName + " (Called on server after Awake() but before Start().)");
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("Player: OnStartLocalPlayer: " + playerName + " (Called on client that owns player after Awake() but before Start().");
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        // This code runs only on the machine that owns the player
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CmdPlaceMarble(10);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            CmdRotateQuadrant(2, ROTATE_DIRECTION.CLOCKWISE);
        }
    }

    public void OnDestroy()
    {
        Debug.Log("Player OnDestroy(): " + playerName);
        playerList.RemovePlayer(this);
    }

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
        board.PlaceMarble(this, spaceIndex);
    }

    public void ExecuteRotateQuadrant(int quadrantIndex, IGame.ROTATE_DIRECTION direction)
    {
        board.RotateQuadrant(this, quadrantIndex, direction);
    }

#pragma warning disable CS0618 // The current networking API is deprecated - suppress this deprecation warning 

    [Command] // This function is called from a single client and is executed on the server
    void CmdChangeName(string newName)
    {
        RpcChangeName(newName);
    }
    [ClientRpc]
    void RpcChangeName(string newName)
    {
        Debug.Log("Player " + netId.ToString() + " changed name to: " + newName);
        playerName = newName;
        gameObject.name = "NetworkPlayer (" + playerName + ")";
    }

    [Command] // This function is called from a single client and is executed on the server
    void CmdPlaceMarble(int spaceIndex)
    {
        RpcPlaceMarble(spaceIndex);
    }
    [ClientRpc]
    void RpcPlaceMarble(int spaceIndex)
    {
        Debug.Log("Player " + netId.ToString() + " executing action: Place marble on space " + spaceIndex);
    }

    [Command] // This function is called from a single client and is executed on the server
    void CmdRotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        RpcRotateQuadrant(quadrantIndex, direction);
    }
    [ClientRpc]
    void RpcRotateQuadrant(int quadrantIndex, ROTATE_DIRECTION direction)
    {
        Debug.Log("Player " + netId.ToString() + " executing action: Rotate quadrant " + quadrantIndex + " " + direction.ToString());
    }

#pragma warning restore CS0618 // The current networking API is deprecated - suppress this deprecation warning 

}
