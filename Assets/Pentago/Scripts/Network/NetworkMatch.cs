using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // The current networking API is deprecated - suppress this deprecation warning 
public class NetworkMatch : NetworkBehaviour
#pragma warning restore CS0618 // The current networking API is deprecated - suppress this deprecation warning
{
    public PentagoNetworkManager network;
    public NetworkPlayerList playerList = null;
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

    private void OnPlayerAdded(NetworkPlayer player)
    {
        if (player.isLocalPlayer)
            player.playerName = localPlayerName;

        var networkLocation = player.isLocalPlayer ? "Local" : "Remote";
        Debug.Log("NetworkMatch OnPlayerAdded(): " + player.playerName + "(" + networkLocation + ")");
    }

    private void OnPlayerRemoved(NetworkPlayer player)
    {
        var networkLocation = player.isLocalPlayer ? "Local" : "Remote";
        Debug.Log("NetworkMatch OnPlayerRemoved(): " + player.playerName + "(" + networkLocation + ")");
    }

    private void OnGUI()
    {
        localPlayerName = GUI.TextField(new Rect(120, 10, 200, 25), localPlayerName, 20);
    }
}
