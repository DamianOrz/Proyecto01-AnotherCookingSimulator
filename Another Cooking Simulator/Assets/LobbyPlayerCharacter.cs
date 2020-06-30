using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LobbyPlayerCharacter : NetworkBehaviour
{
    //Glosario playerType:
    //0 = VR
    //1 = PC
    //2 = undefined --> No eligió aún

    private string playerName; // Player + ID
    private int playerType; // VR o PC
    private bool isReady = false;
    private bool isHost;
    private NetworkRoomPlayer myNetworkRoomPlayer;

    private void Start()
    {
        playerType = 2;
        playerName = "Player" + gameObject.GetComponent<NetworkIdentity>().name;
        
    }

    public void setPlayerType(int id)
    {
        playerType = id;
    }
    public int getPlayerType()
    {
        return playerType;
    }
    
}
