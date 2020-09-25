using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    private int _idCombo;

    private void CrearInterpretacionHS()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        Debug.Log("SIMON 1");
        SetIdCombo(1);
    }
    private void CrearInterpretacionHD()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        Debug.Log("SIMON 2");
        SetIdCombo(2);
    }
    private void CrearInterpretacionHQ()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        Debug.Log("SIMON 3");
        SetIdCombo(3);
    }
    private void CrearInterpretacionHDQ()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        Debug.Log("SIMON 4");
        SetIdCombo(4);
    }
    private void SetIdCombo(int combo)
    {
        _idCombo = combo;   
    }
    private void CrearInterpretacion()
    {
        if (_idCombo == 0) return;
        Debug.Log("SIMON: SE MANDA SOLICITUD PARA CREAR LA INTERPRETACION CON EL COMBO " + _idCombo);
        //PedidoManager.instancePedidoManager.CrearInterpretacion(_idCombo);
        _idCombo = 0;
    }
}
