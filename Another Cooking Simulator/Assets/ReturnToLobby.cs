using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class ReturnToLobby : NetworkBehaviour
{
    NetworkRoomManager myNetworkRoomManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    
    public void BackToLobby()
    {
        BackToLobby2();
    }


    [Server]
    private void BackToLobby2()
    {
        RpcBackToLobby3();
    }

    [Client]
    private void BackToLobby4()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.ServerChangeScene(myNetworkRoomManager.RoomScene);
    }


    [ClientRpc]
    private void RpcBackToLobby3()
    {
        BackToLobby4();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
