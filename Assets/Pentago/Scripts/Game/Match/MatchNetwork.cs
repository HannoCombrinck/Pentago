using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MatchNetwork : NetworkBehaviour, IMatch
{
    public IGame game;
    public IGame Game { get => game; set => game = value; }

    public Board board;
    public PlayerNetworkList playerList = null;

    private void Awake()
    {
        Debug.Assert(playerList != null, "NetworkPlayerList reference required.");
        playerList.Clear();

        Debug.Log("NetworkMatch Awake()");
        /*playerList.onPlayerAdded += OnPlayerAdded;
        playerList.onPlayerRemoved += OnPlayerRemoved;*/
    }

    private void OnDestroy()
    {
        playerList.Clear();
    }

    public void Begin()
    {
        Debug.Log("Network match started");
        RpcBegin();
    }

    public void End()
    {
        Debug.Log("Network match ended");
        gameObject.SetActive(false);
    }

    /*[Command]
    void CmdBegin()
    {
        RpcBegin();
    }*/
    [ClientRpc]
    void RpcBegin()
    {
        Debug.Log("Received rpc that network match has been started.");
        // TODO: start game
        game.StartNewGame();
    }

    [Command]
    void CmdEnd()
    {
        RpcEnd();
    }
    [ClientRpc]
    void RpcEnd()
    {
        Debug.Log("Received rpc that network match has been ended.");
        gameObject.SetActive(false);
    }

    /*public List<IPlayer> GetPlayers()
    {
        return new List<IPlayer>(playerList.remotePlayers)
        {
            playerList.localPlayer
        };
    }*/



    /////////////////////////////////////////////////////////////////////////////////////////
    // TODO: Move player management to lobby away from match?
    /*private void OnPlayerAdded(PlayerNetwork player)
    {
        if (player.isLocalPlayer)
            player.playerName = localPlayerName;

        var networkLocation = player.isLocalPlayer ? "Local" : "Remote";
        Debug.Log("NetworkMatch OnPlayerAdded(): " + player.playerName + "(" + networkLocation + ")");
    }

    private void OnPlayerRemoved(PlayerNetwork player)
    {
        var networkLocation = player.isLocalPlayer ? "Local" : "Remote";
        Debug.Log("NetworkMatch OnPlayerRemoved(): " + player.playerName + "(" + networkLocation + ")");
    }*/
    /////////////////////////////////////////////////////////////////////////////////////////
}
