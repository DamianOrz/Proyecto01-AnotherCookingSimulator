using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class SetNetworkManager : MonoBehaviour
{
    public TMP_Text newAddress;
    public NetworkRoomManager myNetworkRoomManager;
    private string strNewAddress;

    public void GetNewAddress()
    {
        if (newAddress is null)
        {
            strNewAddress = "localhost";
        }
        else
        {
            strNewAddress = newAddress.text;
        }
        
        myNetworkRoomManager.SetNetworkAddress(strNewAddress);
    }


    
        
    
}
