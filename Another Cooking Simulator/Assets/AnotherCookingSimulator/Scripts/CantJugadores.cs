using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class CantJugadores : NetworkBehaviour
{
    int iCantVR = 0;
    int iCantPC = 0;

    int iLimiteJugadoresVR = 1;
    int iLimiteJugadoresPC = 4;

    public TMP_Text tmpJugadoresVR;
    public TMP_Text tmpJugadoresPC;


    [Command]
    public void CmdCheckPlayers(int i)
    {
        if (i == 0) //0 Representa un click en el btn de VR
        {
            if (iCantVR < iLimiteJugadoresVR)
            {
                iCantVR += 1; //Sumo un jugador más
                CmdUpdateVRPlayers(); //Actualizo el texto
                CmdSetPlayerAsVR(); //Le asigno al jugador que presionó el boton un id para luego darle el prefab correcto (En este caso VR)
            }
            else
            {

            }
        }
        else if (i == 1) //1 Representa un click en el btn de PC
        {
            if (iCantPC < iLimiteJugadoresPC)
            {
                iCantPC += 1;
                CmdUpdatePCPlayers();
                CmdSetPlayerAsPC();
            }
        }
    }

    [Command]
    void CmdSetPlayerAsVR()
    {
       //Tengo que asignarle el id y hacer que todos los otros jugadores (menos el player) NO puedan presionar el boton VR 
    }

    [Command]
    void CmdSetPlayerAsPC()
    {
        //Tengo que asignarle el id y hacer que todos los otros jugadores (menos el player) NO puedan presionar el boton PC 
    }

    [Command]
    void CmdUpdatePCPlayers()
    {
        //Actualizo el texto debajo del boton
        tmpJugadoresPC.text = iCantPC + " / " + iLimiteJugadoresPC;
    }

    [Command]
    void CmdUpdateVRPlayers()
    {
        //Actualizo el texto debajo del boton
        tmpJugadoresVR.text = iCantVR + " / " + iLimiteJugadoresVR;
    }
}
