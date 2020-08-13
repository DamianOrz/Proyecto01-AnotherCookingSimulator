using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mirror;

public class PlayerSpawner : NetworkBehaviour
{
    //Online
    private NetworkRoomManager myNetworkRoomManager;
    private NetworkRoomPlayer myPlayer;

    [Header("PcPlayerCanvas")]
    private GameObject canvasCrossHair;
    // Start is called before the first frame update
    void Start()
    {
        canvasCrossHair = GameObject.Find("PCPlayerHud");
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.transform.GetComponent<NetworkIdentity>().isLocalPlayer)
        //{
        //    if(this.transform.GetChild(0).name == "PCPlayer(Clone)")
        //    {
        //        canvasCrossHair.transform.GetChild(0).gameObject.SetActive(true);
        //        this.transform.GetChild(0).transform.GetComponentInChildren<Camera>().enabled = true;
        //        this.transform.GetChild(0).transform.GetChild(1).GetComponent<AudioListener>().enabled = true;
        //    }else
        //    {
        //        canvasCrossHair.transform.GetChild(0).gameObject.SetActive(false);
        //        this.transform.GetChild(0).transform.GetComponentInChildren<Camera>().enabled = false;
        //        this.transform.GetChild(0).transform.GetChild(1).GetComponent<AudioListener>().enabled = false;
        //    }
        //}
    }
}
