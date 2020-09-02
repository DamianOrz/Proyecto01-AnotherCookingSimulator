using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClientesManager : NetworkBehaviour
{
    public static ClientesManager instanceClientesManager;

    private double tiempo = 0; 
    private int contClientes=0;
    private static int pedidosEntregados = 0;

    private double tiempoEntreClientes= 5;
    private bool primeraVez=true;

    private void Awake()
    {
        if(instanceClientesManager!=null && instanceClientesManager!=this)
        {
            Destroy(this);
        }
        else
        {
            instanceClientesManager = this;
        }
    }
    [Server]
    private void Start()
    {
        
    }
    [Server]
    private void Tick()
    {
        if (DiaManager.instanceDiaManager.diaActual > -1)
        {
            //Valido si paso el limite de clientes de ese dia
            if (DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia <= contClientes) return;

            contClientes++;
            PedidoManager.instancePedidoManager.crearPedidoRandom();
        }
    }
    public void playInvokeRepeating(float cadaTantoTiempo)
    {
        contClientes = 0;
        InvokeRepeating("Tick", 0f, cadaTantoTiempo);
    }
    public void cancelInvokeRepeating()
    {
        CancelInvoke("Tick");
    }
    [Server]
    public void seEntregoUnPedido()
    {
        pedidosEntregados++;
    }
}
