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
        RpcBackToLobby3();
        
        
    }


    [Command]
    private void BackToLobby1()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.ServerChangeScene(myNetworkRoomManager.RoomScene);
    }


    [ClientRpc]
    private void RpcBackToLobby3()
    {
        BackToLobby();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
