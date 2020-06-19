using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerCharacter : MonoBehaviour
{
    //0 = VR
    //1 = PC
    //2 = undefined
    public int playerID;

    private void Start()
    {
        playerID = 2;
    }


    public void setID(int id)
    {
        playerID = id;
    }
    public int getID()
    {
        return playerID;
        

    }
}
