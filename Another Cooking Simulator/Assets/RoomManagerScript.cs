using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class RoomManagerScript : NetworkBehaviour
{
    private NetworkRoomManager myNetworkRoomManager;
    private static List<NetworkRoomPlayer> listOfPlayers;
    GameObject myPlayer;

    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;
    [SerializeField]
    GameObject player3;
    [SerializeField]
    GameObject player4;
    [SerializeField]
    GameObject player5;

    // Start is called before the first frame update
    void Awake()
    { //Puede que tenga que mover la funcion de Awake al ClientConnect (o sea, editar el NetworkRoomManager y que en OnCLientConnect o algo así se llame a ShowPlayers(), un metodo publico que puedo crear dentro de esta funcion
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();

        player1.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        player5.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        listOfPlayers = myNetworkRoomManager.roomSlots;
        UpdateCanvas();


    }

    private void UpdateCanvas()
    {
        int num = 1;
        foreach (NetworkRoomPlayer p in listOfPlayers)
        {
            string jugador = "player" + num; //player1 --> player2 --> player3
            
            switch (num)
            {
                case 1:
                    myPlayer = player1;
                    break;
                case 2:
                    myPlayer = player2;
                    break;
                case 3:
                    myPlayer = player3;
                    break;
                case 4:
                    myPlayer = player4;
                    break;
                case 5:
                    myPlayer = player5;
                    break;
            }
            myPlayer.GetComponentInChildren<TMP_Text>().SetText(p.index.ToString());

            //myPlayer.gameObject.transform.GetChild(0); --> 0 = fondo, 1 = nombre, 2 = ready
            myPlayer.gameObject.transform.GetChild(1).GetComponent<TMP_Text>().SetText(p.netId.ToString());

            string isReady;
            if (p.readyToBegin == true)
            {
                isReady = "READY!";
            }
            else
            {
                isReady = "NOT READY";
            }
            myPlayer.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().SetText(isReady);

            myPlayer.SetActive(true);

            num++;
        }
    }
}