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

        if (!this.transform.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponent<AudioListener>().enabled = false;
        }

    }
}
