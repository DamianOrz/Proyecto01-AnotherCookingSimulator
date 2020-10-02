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

    [Server]
    public void ReturnToMainMenu()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.StopHost();
    }


}
