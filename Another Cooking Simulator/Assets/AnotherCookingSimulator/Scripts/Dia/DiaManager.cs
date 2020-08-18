using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaManager : MonoBehaviour
{
    public static DiaManager instanceDiaManager;

    public TMP_Text texto;

    public GameObject contentMostrarPedidoAlVR; 
    public GameObject contentMostrarPedidoCliente;
    public GameObject contentMostrarUltimaInterpretacion;

    public int diaActual = -1;
    private int clientes;

    public enum POSIBLES_INGREDIENTES
    {
        PAN,
        CARNE,
        QUESO,
        CEBOLLA,
        BACON,
        LECHUGA,
        TOMATE
    }

    public DiaInformacion[] diasInfo;
    private void Awake()
    {
        if (instanceDiaManager!=null && instanceDiaManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanceDiaManager = this;
        }
    }
    void Start()
    {
    }

    public void EmpezarDia()
    {
        diaActual++;
        instanceDiaManager.texto.text = "Dia : " + (diaActual);
    }
    public void FinalizarDia()
    {
        int score = ScoreManager.getScore();
        LimpiarPedidos();
        EmpezarDia();
    }
    public void LimpiarPedidos()
    {
        for (int i = 0; i < instanceDiaManager.diasInfo[instanceDiaManager.diaActual].clientesEnElDia; i++)
        {
            if(instanceDiaManager.contentMostrarPedidoAlVR.transform.childCount==3)
            {

            }
            Destroy(instanceDiaManager.contentMostrarPedidoAlVR.transform.GetChild(i).gameObject);
            Destroy(instanceDiaManager.contentMostrarPedidoCliente.transform.GetChild(i).gameObject);
            Destroy(instanceDiaManager.contentMostrarUltimaInterpretacion.transform.GetChild(i).gameObject);
        }
        PedidoManager.instancePedidoManager.LimpiarListaPedidos();
    }
}
