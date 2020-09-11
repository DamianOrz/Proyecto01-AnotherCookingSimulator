using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    private int _idCombo;

    public void CrearInterpretacionHS()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(1);
    }
    public void CrearInterpretacionHD()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(2);
    }
    public void CrearInterpretacionHQ()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(3);
    }
    public void CrearInterpretacionHDQ()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        SetIdCombo(4);
    }
    public void SetIdCombo(int combo)
    {
        _idCombo = combo;
    }
    public void CrearInterpretacion()
    {
        if (_idCombo == 0) return;

        PedidoManager.instancePedidoManager.CrearInterpretacion(_idCombo);
        _idCombo = 0;
    }
}
