using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror; 

public class PlayerSpawner : NetworkBehaviour
{
    public GameObject VRPlayer;
    public GameObject PCPlayer;
    static bool hayPersonajeVR;
    // Start is called before the first frame update
    void Start()
    {
        searchVrPlayer();
        if (hayPersonajeVR)
        {
            if(isLocalPlayer)
            {
                CmdSpawnPCPlayer();
            }           
        }
        else
        {
            CmdSpawnVRPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Command]
    void CmdGrantAuthority(GameObject target)
    {
        // target must have a NetworkIdentity component to be passed through a Command
        // and must already exist on both server and client
        target.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
    [Command]
    void CmdSpawnVRPlayer()
    {
        GameObject go = Instantiate(VRPlayer);
        go.transform.parent = this.transform;

        NetworkServer.Spawn(go,connectionToClient);
    }
    [Command]
    void CmdSpawnPCPlayer()
    {
        GameObject go = Instantiate(PCPlayer);
        go.transform.parent = this.transform;

        NetworkServer.Spawn(go,connectionToClient);
        go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
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
    public bool IsLocal() { return isLocalPlayer; }
}
