using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System;

public class ReturnToLobby : NetworkBehaviour
{
    NetworkRoomManager myNetworkRoomManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void BackToLobby()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        ClientBackToLobby();
        BackToLobby3();
    }

    [Client]
    private void ClientBackToLobby()
    {
        myNetworkRoomManager.StopClient();
    }

    [Server]
    private void BackToLobby3()
    {
        //myNetworkRoomManager.ServerChangeScene(myNetworkRoomManager.RoomScene);
        myNetworkRoomManager.StopHost();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
