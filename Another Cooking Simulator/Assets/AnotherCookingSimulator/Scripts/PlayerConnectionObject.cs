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
        //FindObjectOfType<AudioManager>().Play("Fry");
        //Cada vez que entra un jugador se crea una unidad fisica en el servidor
        if(bandera==true)
        {
            CmdSpawnMyVR();
            bandera = false;
        }
        else
        {
            CmdSpawnMyPC();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    [Command]
    void CmdSpawnMyVR()
    {
        Vector3 position = new Vector3(-1.7f, 1.6f, -2.5f);
        
        GameObject go = Instantiate(VrUnit);
        go.transform.position=position;
        NetworkServer.Spawn(go);
    }
    [Command]
    void CmdSpawnMyPC()
    {
        GameObject go = Instantiate(PcUnit);
        NetworkServer.Spawn(go);
    }
}