using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TomarPedido : MonoBehaviour
{
    private int _idCombo;
    private List<int> _interpretacionDePC = new List<int>();

    //Añadir ingredientes a interpretacion
    public void AñadirPatyAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(1);
    }
    public void AñadirCheddarAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(2);
    }
    public void AñadirCebollaAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(3);
    }
    public void AñadirBaconAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(4);
    }
    public void AñadirLechugaAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(5);
    }

    //limpiar interpretacion
    public void LimpiarInterpretacion()
    {
        _interpretacionDePC.Clear();
    }

    private void AñadirIngredienteAInterpretacion(int idIngrediente)
    {
        _interpretacionDePC.Add(idIngrediente);
    }

    public void SendInterpretacion()
    {
        if (_interpretacionDePC.Count <= 0) return;
        int[] interpretacion;
        
        AñadirPanesAInterpretacion();
        interpretacion = AñadirPanesAInterpretacion();

        PedidoManager.instancePedidoManager.CrearInterpretacion(interpretacion);

        LimpiarInterpretacion();
    }

    private int[] AñadirPanesAInterpretacion()
    {
        int[] interpretacion = new int[_interpretacionDePC.Count + 2];
        interpretacion[0] = 0;
        for(int i = 1 ; i <= _interpretacionDePC.Count ; i++)
        {
            interpretacion[i] = _interpretacionDePC[i - 1];
        }
        interpretacion[interpretacion.Length - 1] = 0;
        return interpretacion;
    }
}
