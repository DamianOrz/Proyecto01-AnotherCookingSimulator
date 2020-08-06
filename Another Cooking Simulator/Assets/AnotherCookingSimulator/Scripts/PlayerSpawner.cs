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
    //Prefabs
    public GameObject VRPlayer;
    public GameObject PCPlayer;


    // Start is called before the first frame update
    void Start()
    {
        
        if (!isLocalPlayer)
        {
            return;
        }

        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        
        foreach (NetworkRoomPlayer p in myNetworkRoomManager.roomSlots)
        {
            if(p.netId == this.netId-26)
            {
                myPlayer = p;
            }
        }
        if(myPlayer.playerType == FindObjectOfType<NetworkRoomPlayer>().playerType)
        {
            if(myPlayer.playerType == 0)
            {
                CmdSpawnVRPlayer();
            }else
            {
                CmdSpawnPCPlayer();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    [Command]
    void CmdSpawnVRPlayer()
    {
        GameObject go = Instantiate(VRPlayer);
        go.transform.parent = this.transform;

        NetworkServer.Spawn(go,connectionToClient);
    }

    [Command]
    void CmdSpawnPCPlayer()
    {
        GameObject go = Instantiate(PCPlayer);
        go.transform.parent = this.transform;

        NetworkServer.Spawn(go,connectionToClient);
        go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    private bool IsLocal() { return isLocalPlayer; }
}
