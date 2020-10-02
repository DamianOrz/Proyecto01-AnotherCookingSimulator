using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class LevelSelectorManager : NetworkBehaviour
{
    private NetworkRoomManager myNetworkRoomManager;
    public GameObject btnDay0; //Tutorial
    public GameObject btnDay1;
    public GameObject btnDay2;
    public GameObject btnDay3;
    public GameObject btnDay4;
    public GameObject btnDay5;

    // Start is called before the first frame update
    void Start()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        btnDay0.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateAllButtons(bool status)
    {
        btnDay0.GetComponent<Button>().interactable = status;
        btnDay1.GetComponent<Button>().interactable = status;
        btnDay2.GetComponent<Button>().interactable = status;
        btnDay3.GetComponent<Button>().interactable = status;
        btnDay4.GetComponent<Button>().interactable = status;
        btnDay5.GetComponent<Button>().interactable = status;
    }



    public void LevelChanged(Button btnSelected)
    {
        UpdateAllButtons(true);

        btnSelected.interactable = false;

        cmdUpdateLevel(btnSelected.GetComponentInChildren<TMP_Text>().text.ToString());

    }

    [Server]
    private void cmdUpdateLevel(string sLevel)
    {
        int iLevel;
        if (sLevel == "Tutorial") //El unico caso, por ahora, en el que el nombre del nivel es distinto al id
        {
            iLevel = 0;
        }
        else
        {
            iLevel = int.Parse(sLevel);
        }
        myNetworkRoomManager.SetLevel(iLevel);
        RpcLevelUpdated(iLevel);
    }


    [ClientRpc]
    private void RpcLevelUpdated(int iLevel)
    {
        //Por ahora no lo usamos pero los clientes verán el nivel seleccionado
        myNetworkRoomManager.LevelSelected = iLevel;
    }




}
