using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class PlayerSpawner : NetworkBehaviour
{
    //Online
    private NetworkRoomManager myNetworkRoomManager;
    private NetworkRoomPlayer myPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            this.transform.GetChild(0).transform.GetComponentInChildren<Camera>().enabled = false;
            this.transform.GetChild(0).transform.GetChild(1).GetComponent<AudioListener>().enabled = false;
        }
    }
}
