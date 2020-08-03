using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class RoomManagerScript : NetworkBehaviour
{
    private NetworkRoomManager myNetworkRoomManager;
    private static List<NetworkRoomPlayer> listOfPlayers;
    NetworkRoomPlayer myPlayer;
    GameObject aPlayer;

    [Header("Player's Canvas")]
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

    [Header("Botones")]
    public Button btnVR;
    public Button btnPC;

    [Header("TMP_Text")]
    public TMP_Text tmpJugadoresVR;
    public TMP_Text tmpJugadoresPC;

    int iCantVR = 0;
    int iCantPC = 0;

    int iLimiteJugadoresVR = 1;
    int iLimiteJugadoresPC = 4;

    

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

        player1.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        player5.SetActive(false);

        foreach (NetworkRoomPlayer p in listOfPlayers)
        {
            switch (num)
            {
                case 1:
                    aPlayer = player1;
                    break;
                case 2:
                    aPlayer = player2;
                    break;
                case 3:
                    aPlayer = player3;
                    break;
                case 4:
                    aPlayer = player4;
                    break;
                case 5:
                    aPlayer = player5;
                    break;
                default:

                    break;

            }

            aPlayer.GetComponentInChildren<TMP_Text>().SetText(p.index.ToString());

            //myPlayer.gameObject.transform.GetChild(0); --> 0 = fondo, 1 = nombre, 2 = ready
            aPlayer.gameObject.transform.GetChild(1).GetComponent<TMP_Text>().SetText(p.netId.ToString());

            string isReady;
            if (p.readyToBegin == true)
            {
                isReady = "READY!";
            }
            else
            {
                isReady = "NOT READY";
            }
            aPlayer.gameObject.transform.GetChild(2).GetComponent<TMP_Text>().SetText(isReady);

            aPlayer.SetActive(true);


            //Obtengo MI jugador
            if (p.isLocalPlayer)
            {
                myPlayer = p;
                aPlayer.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            }

            num++;
        }

    }

    [Client]
    public void SetReadyState()
    {
        myPlayer.CmdCUSTOMChangeReadyState(!myPlayer.readyToBegin); //Revisar NetworkRoomPlayer para ver funcion, evita que inicie el juego de forma automatica
    }

    [Server]
    public void StartGame() // --> Solo el host puede correr este script
    {
        myNetworkRoomManager.CheckReadyToBegin();
    }

    //Glosario playerType:
    //0 = VR
    //1 = PC
    //2 = undefined --> No eligió aún

    public void SetVrPlayer()
    {
        if (!(iCantVR < iLimiteJugadoresVR))
        {
            return;
        }

        if(myPlayer.playerType == 2)
        {
            iCantVR++;
        }
        else
        {
            iCantVR++;
            iCantPC--;
        }

        CmdUpdateVRButton();

        CmdSetPlayerAsVR();
        CmdUpdateVRPlayers();
        

        btnPC.enabled = false;
    }

    [Client]
    public void CmdSetPlayerAsVR()
    {
        //Seteo el player como tipo VR
        myPlayer.playerType = 0;
    }

    [Command]
    public void CmdUpdateVRButton()
    {
        //Desabilito los botones
        btnVR.enabled = false;
    }

    [Client]
    void CmdUpdateVRPlayers()
    {
        //Actualizo el texto debajo del boton
        tmpJugadoresVR.text = iCantVR + " / " + iLimiteJugadoresVR;
    }

    public void SetPcPlayer()
    {
        if (!(iCantPC < iLimiteJugadoresPC))
        {
            return;
        }

        if (myPlayer.playerType == 2)
        {
            iCantPC++;
        }
        else
        {
            iCantPC++;
            iCantVR--;
        }

        if(iCantPC == iLimiteJugadoresPC)
        {
            CmdUpdatePCButton();
            btnVR.enabled = true;
        }

        CmdSetPlayerAsPC();
        CmdUpdatePCPlayers();
    }

    [Command]
    public void CmdUpdatePCButton()
    {
        //Desabilito los botones
        btnPC.enabled = false;
    }

    [Client]
    void CmdSetPlayerAsPC()
    {
        iCantPC += 1;
        myPlayer.playerType = 1;
        //Tengo que asignarle el id y hacer que todos los otros jugadores (menos el player) NO puedan presionar el boton PC
        btnPC.enabled = false;
    }

    [Client]
    void CmdUpdatePCPlayers()
    {
        //Actualizo el texto debajo del boton
        tmpJugadoresPC.text = iCantPC + " / " + iLimiteJugadoresPC;
    }
}