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
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        mySteamManager = myNetworkRoomManager.GetComponent<SteamManager>();
        if (newAddress is null)
        {
            strNewAddress = "localhost";
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[0] = localTransport;
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[1] = steamTransport;

        }
        else
        {
            strNewAddress = newAddress.GetParsedText().ToString();
            strNewAddress = strNewAddress.Substring(0, 17); //Hay algún componente de rich text?
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[1] = localTransport;
            myNetworkRoomManager.GetComponent<MultiplexTransport>().transports[0] = steamTransport;
        }
        Debug.Log(strNewAddress);
        Debug.Log(strNewAddress.Length);
        myNetworkRoomManager.SetNetworkAddress(strNewAddress);
        myNetworkRoomManager.StartClient();
    }


    
        
    
}
