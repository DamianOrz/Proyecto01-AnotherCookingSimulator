using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class PlayerSpawner : NetworkBehaviour
{
    public GameObject VRPlayer;
    public GameObject PCPlayer;
    static bool hayPersonajeVR;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        searchVrPlayer();
        if (hayPersonajeVR)
        {
            NetworkServer.ReplacePlayerForConnection(this.GetComponentInParent<NetworkBehaviour>().connectionToClient, Instantiate(PCPlayer));
        }
        else
        {
            NetworkServer.ReplacePlayerForConnection(this.GetComponentInParent<NetworkBehaviour>().connectionToClient, Instantiate(VRPlayer));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void searchVrPlayer()
    {
        bool seEncontro = false;
        GameObject obj = GameObject.FindGameObjectWithTag("VRPlayer");
        if (obj)
        {
            hayPersonajeVR = true;
        }
    }
}
