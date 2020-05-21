using UnityEngine.Audio;
using System;
using UnityEngine;

public class PedidoManager : MonoBehaviour
{
    private static string[] _posiblesOrdenes=new string[] 
    {
        "pan, carne, pan",
        "pan, carne, queso, pan",
        "pan, queso, carne, pan"
    };
    public static Pedido crearPedidoRandom()
    {
        float fIndiceRandom = UnityEngine.Random.Range(0f,2f);
        int iIndiceRandom = Convert.ToInt32(fIndiceRandom);
        Pedido unPedido=new Pedido(_posiblesOrdenes[iIndiceRandom]);
        return unPedido;
    }
}
