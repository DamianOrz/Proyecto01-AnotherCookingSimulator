using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnectionObject : NetworkBehaviour
{
    static bool bandera = true;
    public GameObject VrUnit;
    public GameObject PcUnit;
    // Start is called before the first frame update
    void Start()
    {
        //Cada vez que entra un jugador se crea una unidad fisica en el servidor
        if(bandera==true)
        {
            CmdSpawnMyVR();
            bandera = false;
        }
        else
        {
          CmdSpawnMyPC();
          bandera = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    [Command]
    void CmdSpawnMyVR()
    {
        GameObject go = Instantiate(VrUnit);

        NetworkServer.Spawn(go);
    }
    [Command]
    void CmdSpawnMyPC()
    {
        GameObject go = Instantiate(PcUnit);
        NetworkServer.Spawn(go);
    }
}