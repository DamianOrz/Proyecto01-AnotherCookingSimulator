using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnectionObject : NetworkBehaviour
{
    private static bool bandera = true;

    public GameObject[] PlayerArray;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            return;
        }
        //Cada vez que entra un jugador se crea una unidad fisica en el servidor
        CmdSpawnPlayers();
    }
    // Update is called once per frame
    void Update()
    {

    }
    [Command]
    void CmdSpawnPlayers()
    {
        if (base.hasAuthority)
        {
            GameObject go = Instantiate(PlayerArray[0]);
            //go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            NetworkServer.Spawn(go, connectionToClient);
            bandera = false;
        }else
        {
            GameObject go = Instantiate(PlayerArray[1]);
            //go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
            NetworkServer.Spawn(go,connectionToClient);
        }
    } 
}