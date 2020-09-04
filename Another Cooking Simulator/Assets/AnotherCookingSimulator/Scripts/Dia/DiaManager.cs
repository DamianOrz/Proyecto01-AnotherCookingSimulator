using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Mirror;

public class DiaManager : NetworkBehaviour
{
    public static DiaManager instanceDiaManager;

    public TMP_Text textoDia;
    
    public GameObject contentMostrarPedidoAlVR; 
    public GameObject contentMostrarPedidoCliente;
    public GameObject contentMostrarUltimaInterpretacion;

    private float contadorDelDia = 0;

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
    //Mostrar Clock

    public TMP_Text tiempo;
    //Canvas finalizacion del dia
    public TMP_Text titulo;
    public Canvas canvasAlFinalizarDia;
    public TMP_Text mostrarPuntos;

    [Server]
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
    [Server]
    void Start()
    {
        instanceDiaManager.EmpezarDia();
        canvasAlFinalizarDia.enabled = false;
    }
    [Server]
    private void Update()
    {
        if (canvasAlFinalizarDia.enabled)
        {
            return;
        }

        tiempo.text =(instanceDiaManager.diasInfo[instanceDiaManager.diaActual].duracionDelDia - contadorDelDia).ToString();

        if (contadorDelDia < instanceDiaManager.diasInfo[instanceDiaManager.diaActual].duracionDelDia && !isCanvasBeingUsed())
        {
            contadorDelDia += Time.deltaTime;
        }
        else {
            FinalizarDia();
        }
    }
    [Server]
    public bool isCanvasBeingUsed()
    {
        if (canvasAlFinalizarDia.enabled) return true;

        return false;
    }
    [Server]
    public void EmpezarDia()
    {
        contadorDelDia = 0;

        diaActual++;
        //instanceDiaManager.textoDia.text = "Dia : " + diaActual;
        RpcPrintDiaActual();
        //Empiezo la emision de pedidos
        ClientesManager.instanceClientesManager.playInvokeRepeating(instanceDiaManager.diasInfo[instanceDiaManager.diaActual].ratioDePedidos);

        //Apago el canvas
        UpdateCanvasStatus();
    }
    [ClientRpc]
    private void RpcPrintDiaActual()
    {
        instanceDiaManager.textoDia.text = "Dia : " + diaActual;
    }
    [Server]
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    [Server]
    public void FinalizarDia()
    {
        //Paro la emision de pedidos
        ClientesManager.instanceClientesManager.cancelInvokeRepeating();

        //Muestro canvas y muestro puntos
        int score = ScoreManager.getScore();
        mostrarPuntos.text = score.ToString();

        RpcPrintFinalizacionDelDia();
        //Pause();
        UpdateCanvasStatus();
        limpiarContent();

        
        //LimpiarPedidos();
    }
    [ClientRpc]
    public void RpcPrintFinalizacionDelDia()
    {
        titulo.text = "Dia " + diaActual + " finalizado!!!";
    }
    [Server]
    private void UpdateCanvasStatus()
    {
        canvasAlFinalizarDia.enabled = !canvasAlFinalizarDia.enabled;

        Cursor.lockState = CursorLockMode.None;

        if (canvasAlFinalizarDia.enabled)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    [Server]
    public void limpiarContent()
    {
        int cantOfChild = PedidoManager.instancePedidoManager.contentMostrarPedidoCliente.transform.childCount;
        for (int i = 0; i < cantOfChild; i++)
        {
            Destroy(PedidoManager.instancePedidoManager.contentMostrarPedidoCliente.transform.GetChild(i).gameObject);
        }
    }
    [Server]
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
