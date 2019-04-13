using UnityEngine;
using UnityEngine.Networking;
using static IGame;

#pragma warning disable CS0618 // Type or member is obsolete
public class NetworkPlayer : NetworkBehaviour
#pragma warning restore CS0618 // Type or member is obsolete
{
    public NetworkPlayerList playerList = null;

    // TODO: Don't use SyncVar, rather use a network Command to change the name
    #pragma warning disable CS0618 // Type or member is obsolete
    [SyncVar] // TODO: Why isn't hook working?
    #pragma warning restore CS0618 // Type or member is obsolete
    public string playerName;

    private void Awake()
    {
        Debug.Assert(playerList != null, "NetworkPlayerList reference required.");
        Debug.Log("Player Awake()");
    }

    private void Start()
    {
        playerList.AddPlayer(this);
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        // This code runs only on the machine that owns the player
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdPlaceMarble(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CmdRotateQuadrant(2, ROTATE_DIRECTION.CLOCKWISE);
        }
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

    public void OnDestroy()
    {
        Debug.Log("Player OnDestroy(): " + playerName);
        playerList.RemovePlayer(this);
    }
}
