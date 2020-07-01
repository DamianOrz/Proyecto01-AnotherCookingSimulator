using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GetNetworkRoomManager : NetworkBehaviour // Antes era NetworkBehaviour
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
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.useGUILayout = false;
        myNetworkRoomManager.StopHost();
        
    }

    public void ActivatePlayerGUI()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        myNetworkRoomManager.useGUILayout = true;
    }
}
