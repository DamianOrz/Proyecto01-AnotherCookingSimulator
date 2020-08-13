using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientesManager : MonoBehaviour
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
    // Update is called once per frame
    void Update()
    {
        if (DiaManager.instanceDiaManager.diaActual>-1)
        {
            if (primeraVez)
            {
                PedidoManager.instancePedidoManager.crearPedidoRandom();
                contClientes++;
                primeraVez = false;
            }
            if(!(DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia==contClientes))
            {
                if (contClientes == pedidosEntregados)
                {
                    tiempo += Time.deltaTime;
                }
                if (tiempo>=tiempoEntreClientes)
                {
                    contClientes++;
                    PedidoManager.instancePedidoManager.crearPedidoRandom();
                    tiempo = 0;
                }
            }
        }
    }
    public void seEntregoUnPedido()
    {
        pedidosEntregados++;
    }
}
