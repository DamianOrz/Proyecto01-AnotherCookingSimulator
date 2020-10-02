using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class LevelSelectedScript : NetworkBehaviour
{
    private NetworkRoomManager myNetworkRoomManager;
    // Start is called before the first frame update
    void Awake()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int iLevelSelected = myNetworkRoomManager.GetLevel();
        if (iLevelSelected == 0)
        {
            this.gameObject.GetComponent<TMP_Text>().SetText("Level Selected: TUTORIAL");
        }
        else
        {
            this.gameObject.GetComponent<TMP_Text>().SetText("Level Selected: " + iLevelSelected.ToString());
        }
    }
}
