using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerNetworkList : ScriptableObject
{
    public PlayerNetwork localPlayer;
    public List<PlayerNetwork> remotePlayers;

    public delegate void PlayerListEvent(PlayerNetwork player);
    public PlayerListEvent onPlayerAdded;
    public PlayerListEvent onPlayerRemoved;

    public void Clear()
    {
        localPlayer = null;
        remotePlayers.Clear();
    }

    public void AddPlayer(PlayerNetwork player)
    {
        if (player.isLocalPlayer)
        {
            localPlayer = player;
            Debug.Log("NetworkPlayerList OnLocalPlayerAdded");
        }
        else
        {
            remotePlayers.Add(player);
            Debug.Log("NetworkPlayerList OnRemotePlayerAdded");
        }

        onPlayerAdded?.Invoke(player);
    }

    public void RemovePlayer(PlayerNetwork player)
    {
        onPlayerRemoved?.Invoke(player);

        if (player.isLocalPlayer)
        {
            Debug.Log("NetworkPlayerList OnLocalPlayerRemoved");
            localPlayer = null;
        }
        else
        {
            Debug.Log("NetworkPlayerList OnRemotePlayerRemoved");
            remotePlayers.Remove(player);
        }
    }
    
}
