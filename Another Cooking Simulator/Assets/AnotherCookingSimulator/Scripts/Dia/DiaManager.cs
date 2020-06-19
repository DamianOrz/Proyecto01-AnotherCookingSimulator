using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaManager : MonoBehaviour
{
    public TMP_Text texto;
    public static TMP_Text textoStc;

    public GameObject contentMostrarPedidoAlVR;
    public GameObject contentMostrarPedidoCliente;
    public GameObject contentMostrarUltimaInterpretacion;

    public static GameObject contentMostrarCliente;
    public static GameObject contentMostrarVR;
    public static GameObject contentUltimaInterpretacion;

    public static int diaActual = -1;
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

    public static DiaInformacion[] diasInfoStc;
    public DiaInformacion[] diasInfo;
    void Start()
    {
        diasInfoStc = diasInfo;
        textoStc = texto;
        contentMostrarVR = contentMostrarPedidoAlVR;
        contentMostrarCliente = contentMostrarPedidoCliente;
        contentUltimaInterpretacion = contentMostrarUltimaInterpretacion;
    }

    public static void EmpezarDia()
    {
        diaActual++;
        textoStc.text = "Dia : " + (diaActual);
    }
    public static void FinalizarDia()
    {
        int score = ScoreManager.getScore();
        LimpiarPedidos();
        EmpezarDia();
    }
    public static void LimpiarPedidos()
    {
        for (int i = 0; i < DiaManager.diasInfoStc[DiaManager.diaActual].clientesEnElDia; i++)
        {
            if(contentMostrarVR.transform.childCount==3)
            {

            }
            Destroy(contentMostrarVR.transform.GetChild(i).gameObject);
            Destroy(contentMostrarCliente.transform.GetChild(i).gameObject);
            Destroy(contentUltimaInterpretacion.transform.GetChild(i).gameObject);
        }
        PedidoManager.LimpiarListaPedidos();
    }
}
