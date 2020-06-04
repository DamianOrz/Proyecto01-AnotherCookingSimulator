using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PedidoManager : MonoBehaviour
{
    //private List<String> diaUnoOpciones = new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan"};
    //private List<String> diaDosOpciones = new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan", "pan , queso, cebolla carne, pan", "pan, queso, cebolla, carne, bacon, pan" };
    //private List<String> diaTresOpciones = new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan", "pan , queso, cebolla carne, pan", "pan, queso, cebolla, carne, bacon, pan", "pan ,lechuga, queso, cebolla, carne, bacon, pan" };

    [SerializeField] private static List<Pedido> listaPedidos = new List<Pedido>();
    //List<string>[] posiblesOrdenes = new List<string>[]();
    List<String>[] _posiblesOrdenes = new List<String>[]
    {
    new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan"} ,
    new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan", "pan , queso, cebolla carne, pan", "pan, queso, cebolla, carne, bacon, pan" },
    new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan", "pan , queso, cebolla carne, pan", "pan, queso, cebolla, carne, bacon, pan", "pan ,lechuga, queso, cebolla, carne, bacon, pan" }
    };

    public static void crearPedidoRandom(int dia)
    {
        float fIndiceRandom = UnityEngine.Random.Range(0f, 2f);
        int iIndiceRandom = Convert.ToInt32(fIndiceRandom);

        Pedido unPedido = new Pedido(_posiblesOrdenes[iIndiceRandom]);
        listaPedidos.Add(unPedido);
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
    public static void ActualizarContenidoDePantalla()
    {
        GenerarPedido.GenerarPedidoCanvas(pedido);
    }
}