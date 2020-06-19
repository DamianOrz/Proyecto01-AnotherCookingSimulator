using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientesManager : MonoBehaviour
{
    private double tiempo = 0; 
    private int contClientes=0;
    private static int pedidosEntregados = 0;

    private double tiempoEntreClientes= 10;
    private bool primeraVez=true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DiaManager.diaActual>-1)
        {
            if (primeraVez)
            {
                PedidoManager.crearPedidoRandom();
                contClientes++;
                primeraVez = false;
            }
            if(!(DiaManager.diasInfoStc[DiaManager.diaActual].clientesEnElDia==contClientes))
            {
                tiempo += Time.deltaTime;
                if (tiempo>=tiempoEntreClientes && contClientes==pedidosEntregados)
                {
                    contClientes++;
                    PedidoManager.crearPedidoRandom();
                    tiempo = 0;
                }
            }
        }
    }
    public static void seEntregoUnPedido()
    {
        pedidosEntregados++;
    }
}
