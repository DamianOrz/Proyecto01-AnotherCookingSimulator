using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class SetNetworkManager : MonoBehaviour
{
    public TMP_Text newAddress;
    public NetworkRoomManager myNetworkRoomManager;
    public SteamManager mySteamManager;

    private Transport localTransport;
    private Transport steamTransport;

    Transport transportAUtilizar;
    private string strNewAddress;

    private void Start()
    {
        localTransport = myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[0];
        steamTransport = myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[1];
    }

    public void GetNewAddress()
    {
        if (newAddress is null)
        {
            strNewAddress = "localhost";
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[0] = localTransport;
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[1] = steamTransport;

        }
        else
        {
            strNewAddress = newAddress.text;
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[1] = localTransport;
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[0] = steamTransport;
        }

        myNetworkRoomManager.networkAddress = strNewAddress;
        myNetworkRoomManager.StartClient();
    }


    
        
    
}
