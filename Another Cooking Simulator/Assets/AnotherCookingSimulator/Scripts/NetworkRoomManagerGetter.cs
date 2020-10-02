using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkRoomManagerGetter : MonoBehaviour
{
    NetworkRoomManager myNetworkRoomManager;
    public void StartHost()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.StartHost();
    }
}
