﻿using System.Collections;
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

    private NetworkRoomManager myNetworkRoomManager;

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
    public Image relojTiempoRestante;

    //Canvas finalizacion del dia
    public TMP_Text titulo;
    public Canvas canvasAlFinalizarDia;
    public TMP_Text mostrarPuntos;
    public GameObject prefabVehicle1; //El auto verde

    //Canvas
    private GameObject _canvasOrderCreator;
    private List<Button> _buttonList = new List<Button>();

    private void Awake()
    {
        myNetworkRoomManager = FindObjectOfType<NetworkRoomManager>();
        if (instanceDiaManager != null && instanceDiaManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanceDiaManager = this;
        }
        diaActual = myNetworkRoomManager.GetLevel(); //Devuelve el dia/nivel que se está jugando
        _canvasOrderCreator = GameObject.Find("LeftColumn").gameObject;
        foreach (Transform child in _canvasOrderCreator.transform.GetChild(0).transform)
        {
            if (child.gameObject.name.Contains("btn"))
            {
                _buttonList.Add(child.GetComponent<Button>());
            }
        }   
    }
    void Start()
    {
        instanceDiaManager.EmpezarDia();
        canvasAlFinalizarDia.enabled = false;
    }

    private void Update()
    {
        if (canvasAlFinalizarDia.enabled)
        {
            return;
        }
        //Los paso a int
        int iContador = (int)contadorDelDia;

        float fDuracionDia = instanceDiaManager.diasInfo[instanceDiaManager.diaActual].duracionDelDia;
        int iDuracionDia = (int)fDuracionDia;

        

        tiempo.text = (iDuracionDia - iContador).ToString();
        float FillAmount = (fDuracionDia - contadorDelDia) / fDuracionDia;
        relojTiempoRestante.fillAmount = FillAmount;

        if (contadorDelDia < instanceDiaManager.diasInfo[instanceDiaManager.diaActual].duracionDelDia && !isCanvasBeingUsed())
        {
            contadorDelDia += Time.deltaTime;
        }
        else
        {
            FinalizarDia();
        }
    }
    private void UpdateOrderCreatorCanvas()
    {
        POSIBLES_INGREDIENTES[] listaDePosiblesIngredientesDelDia = diasInfo[diaActual].posiblesIngredientes;
        for (int i = 1; i < _buttonList.Count; i++)
        {
            _buttonList[i].interactable = false;
        }
        for (int i = 1; i < listaDePosiblesIngredientesDelDia.Length; i++)
        {
            _buttonList[(int)listaDePosiblesIngredientesDelDia[i]].interactable = true;
        }
    }
    public bool isCanvasBeingUsed()
    {
        if (canvasAlFinalizarDia.enabled) return true;

        return false;
    }

    public void EmpezarDia()
    {
        contadorDelDia = 0;

        diaActual = myNetworkRoomManager.GetLevel();
        //instanceDiaManager.textoDia.text = "Dia : " + diaActual;
        //Empiezo la emision de pedidos
        ClientesManager.instanceClientesManager.playInvokeRepeating(instanceDiaManager.diasInfo[instanceDiaManager.diaActual].ratioDePedidos);
        //Apago el canvas
        //UpdateOrderCreatorCanvas();
        UpdateCanvasStatus();
    }

    [Server]
    public void EmpezarDiaServer()
    {
        RpcEmpezarDia();
    }

    [ClientRpc]
    public void RpcEmpezarDia()
    {
        contadorDelDia = 0;
        diaActual = myNetworkRoomManager.LevelSelected;
        UpdateOrderCreatorCanvas();
        instanceDiaManager.textoDia.text = "Dia : " + diaActual;
        ClientesManager.instanceClientesManager.playInvokeRepeating(instanceDiaManager.diasInfo[instanceDiaManager.diaActual].ratioDePedidos);

        /*
        GameObject go;
        for (int i = 0; i < 5; i++)
        {
            go = Instantiate(prefabVehicle1);
            go.transform.position = new Vector3(0.3f, 6, 60);
            NetworkServer.Spawn(go);
        }*/

        //Apago el canvas
        UpdateCanvasStatus();
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void FinalizarDia()
    {
        //Paro la emision de pedidos
        ClientesManager.instanceClientesManager.cancelInvokeRepeating();

        //Muestro canvas y muestro puntos
        int score = ScoreManager.getScore();
        mostrarPuntos.text = score.ToString();
        titulo.text = "Dia " + diaActual + " finalizado!!!";
        //Pause();
        UpdateCanvasStatus();
        limpiarContent();


        //LimpiarPedidos();
    }

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

    public void limpiarContent()
    {
        int cantOfChild = PedidoManager.instancePedidoManager.contentMostrarPedidoCliente.transform.childCount;
        for (int i = 0; i < cantOfChild; i++)
        {
            Destroy(PedidoManager.instancePedidoManager.contentMostrarPedidoCliente.transform.GetChild(i).gameObject);
        }
    }

    public void LimpiarPedidos()
    {
        for (int i = 0; i < instanceDiaManager.diasInfo[instanceDiaManager.diaActual].clientesEnElDia; i++)
        {
            if (instanceDiaManager.contentMostrarPedidoAlVR.transform.childCount == 3)
            {

            }
            Destroy(instanceDiaManager.contentMostrarPedidoAlVR.transform.GetChild(i).gameObject);
            Destroy(instanceDiaManager.contentMostrarPedidoCliente.transform.GetChild(i).gameObject);
            Destroy(instanceDiaManager.contentMostrarUltimaInterpretacion.transform.GetChild(i).gameObject);
        }
        PedidoManager.instancePedidoManager.LimpiarListaPedidos();
    }

    public void setearNivel(int i)
    {
        diaActual = i;
    }
}