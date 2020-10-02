using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GetNetworkRoomManager : NetworkBehaviour
{
    private NetworkRoomManager myNetworkRoomManager;
    // Start is called before the first frame update
    void Start()
    {
            myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        HostToMainMenu();
        ClientToMainMenu();
    }

    [Client]
    private void ClientToMainMenu()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.StopClient();
    }

    [Server]
    private void HostToMainMenu()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.StopHost();
    }

}
