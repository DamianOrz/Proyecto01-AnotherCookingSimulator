using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayersHUD : NetworkBehaviour
{
    private NetworkRoomManager myNetworkRoomManager;
    private List<NetworkRoomPlayer> listOfPlayers;
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
    { //Puede que tenga que mover la funcion e Awake al ClientConnect (o sea, editar el NetworkRoomManager y que en OnCLientConnect o algo así se llame a ShowPlayers(), un metodo publico que puedo crear dentro de esta funcion
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        listOfPlayers = myNetworkRoomManager.roomSlots;

        int i = 0;
        foreach (NetworkRoomPlayer p in listOfPlayers)
        {
            switch (i)
            {
                case 1:

                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
