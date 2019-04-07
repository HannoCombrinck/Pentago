using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
public class Player : NetworkBehaviour
#pragma warning restore CS0618 // Type or member is obsolete
{
    #pragma warning disable CS0618 // Type or member is obsolete
    [SyncVar]
    #pragma warning restore CS0618 // Type or member is obsolete
    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        #pragma warning disable CS0618 // Type or member is obsolete
        var networkID = GetComponent<NetworkIdentity>();
        #pragma warning restore CS0618 // Type or member is obsolete

        var netIDString = "Player " + networkID.netId.ToString();
        Debug.Log(playerName + ", network ID: " + netIDString);
        gameObject.name = playerName;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("OnStartClient: " + playerName);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("OnStartLocalPlayer: " + playerName);
    }

    public void OnDestroy()
    {
        Debug.Log("OnDestroy: " + playerName);
    }
}
