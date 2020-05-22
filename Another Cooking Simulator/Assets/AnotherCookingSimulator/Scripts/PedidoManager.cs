using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PedidoManager : MonoBehaviour
{
    [SerializeField] private static List<Pedido> listaPedidos = new List<Pedido>();

    private static string[] _posiblesOrdenes = new string[] { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan" };

    public static void crearPedidoRandom()
    {
        float fIndiceRandom = UnityEngine.Random.Range(0f, 2f);
        int iIndiceRandom = Convert.ToInt32(fIndiceRandom);

        Pedido unPedido = new Pedido(_posiblesOrdenes[iIndiceRandom]);
        listaPedidos.Add(unPedido);
        //return unPedido;
    }
    public static Pedido crearPedidoConStringOrdenIngredientes(string ordenDeIngredientes)
    {
        Pedido unPedido = new Pedido(ordenDeIngredientes);
        return unPedido;
    }
    public static List<Pedido> getListaPedidos()
    {
        List<Pedido> pedidos = listaPedidos;
        return pedidos;
    }
}