using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkLobby : MonoBehaviour
{
    public string localPlayerName;
    public int NumberOfPlayers = 2;
    public PlayerNetworkList playerList;

    public List<IPlayer> gamePlayers = new List<IPlayer>();
    public List<IPlayer> gameObservers = new List<IPlayer>();

    private void Awake()
    {
        Debug.Assert(playerList != null, "NetworkPlayerList reference required.");
        playerList.Clear();
        playerList.onPlayerAdded += OnPlayerAdded;
        playerList.onPlayerRemoved += OnPlayerRemoved;
    }

    private void OnDestroy()
    {
        playerList.Clear();
    }

    private void OnPlayerAdded(PlayerNetwork player)
    {
        if (player.isLocalPlayer)
            player.playerName = localPlayerName;

        var networkLocation = player.isLocalPlayer ? "Local" : "Remote";
        Debug.Log("NetworkLobby OnPlayerAdded(): " + player.playerName + "(" + networkLocation + ")");
    }

    private void OnPlayerRemoved(PlayerNetwork player)
    {
        var networkLocation = player.isLocalPlayer ? "Local" : "Remote";
        Debug.Log("NetworkLobby OnPlayerRemoved(): " + player.playerName + "(" + networkLocation + ")");
    }
}
