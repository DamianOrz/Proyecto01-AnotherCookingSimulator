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
    #region Start
    private void Start()
    {
        InvokeRepeating("Tick", 0f, 25f);
    }
    private void Tick()
    {
        if (DiaManager.instanceDiaManager.diaActual > -1)
        {
            if (!(DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia == contClientes))
            {
                contClientes++;
                PedidoManager.instancePedidoManager.crearPedidoRandom();
            }
            else if (DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia == contClientes)
            {
                DiaManager.instanceDiaManager.FinalizarDia();
            }
        }
    }
    #endregion
    // Update is called once per frame
    void Update()
    {

    }

    public void seEntregoUnPedido()
    {
        pedidosEntregados++;
    }
}
