using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MatchNetwork : NetworkBehaviour
{
    public Match match;
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
        match.Begin();
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
        match.End();
        gameObject.SetActive(false);
    }

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
