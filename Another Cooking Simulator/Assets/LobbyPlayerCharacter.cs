using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerCharacter : MonoBehaviour
{
    //Glosario playerType:
    //0 = VR
    //1 = PC
    //2 = undefined --> No eligió aún

    private string playerName; // Player + ID
    private int playerType; // VR o PC
    private bool isReady;
    private bool isHost;

    private void Start()
    {
        playerType = 2;
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
