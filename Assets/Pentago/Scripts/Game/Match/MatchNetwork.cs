using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // The current networking API is deprecated - suppress this deprecation warning 
public class MatchNetwork : NetworkBehaviour, IMatch
#pragma warning restore CS0618 // The current networking API is deprecated - suppress this deprecation warning
{
    public PentagoNetworkManager network;
    public PlayerNetworkList playerList = null;
    public string localPlayerName;

    private void Awake()
    {
        Debug.Assert(playerList != null, "NetworkPlayerList reference required.");
        playerList.Clear();

        Debug.Log("NetworkMatch Awake()");
        playerList.onPlayerAdded += OnPlayerAdded;
        playerList.onPlayerRemoved += OnPlayerRemoved;
    }

    private void OnDestroy()
    {
        playerList.Clear();
    }

    public void Begin()
    {
        Debug.Log("Network match started");
        // TODO:
    }

    public void End()
    {
        Debug.Log("Network match ended");
        gameObject.SetActive(false);
    }

    public List<IPlayer> GetPlayers()
    {
        return new List<IPlayer>(playerList.remotePlayers)
        {
            playerList.localPlayer
        };
    }

    private void OnPlayerAdded(PlayerNetwork player)
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
    }
}
