using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    private int _idCombo;

    private void CrearInterpretacionHS()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(1);
    }
    private void CrearInterpretacionHD()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(2);
    }
    private void CrearInterpretacionHQ()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(3);
    }
    private void CrearInterpretacionHDQ()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(4);
    }
    private void SetIdCombo(int combo)
    {
        _idCombo = combo;
    }
    private void CrearInterpretacion()
    {
        if (_idCombo == 0) return;

        PedidoManager.instancePedidoManager.CrearInterpretacion(_idCombo);
        _idCombo = 0;
    }
}
