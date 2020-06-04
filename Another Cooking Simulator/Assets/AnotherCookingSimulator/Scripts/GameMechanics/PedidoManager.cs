using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PedidoManager : MonoBehaviour
{
    public static GameObject content;
    public static GameObject prefab;
    static int iNumPedido = 1;
    //private List<String> diaUnoOpciones = new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan"};
    //private List<String> diaDosOpciones = new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan", "pan , queso, cebolla carne, pan", "pan, queso, cebolla, carne, bacon, pan" };
    //private List<String> diaTresOpciones = new List<String>() { "pan, carne, pan", "pan, carne, queso, pan", "pan, queso, carne, pan", "pan , queso, cebolla carne, pan", "pan, queso, cebolla, carne, bacon, pan", "pan ,lechuga, queso, cebolla, carne, bacon, pan" };

    [SerializeField] private static List<Pedido> _listaPedidos = new List<Pedido>();
    //List<string>[] posiblesOrdenes = new List<string>[]();
    private static List<String>[] _posiblesIngredientes = new List<String>[]
    {
    new List<String>() { "queso","carne"} ,
    new List<String>() { "bacon", "queso","carne", "cebolla" },
    new List<String>() { "bacon", "queso", "carne", "cebolla" ,"lechuga"},
    new List<String>() { "bacon", "queso", "carne", "cebolla" ,"lechuga","tomate"},
    new List<String>() { "bacon", "queso", "carne", "cebolla" ,"lechuga","tomate","ketchup","mayonesa"}
    };

    public static void crearPedidoRandom(int level)
    {
        float fIndiceRandom = UnityEngine.Random.Range(0f, 2f);
        int iIndiceRandom = Convert.ToInt32(fIndiceRandom);
        Pedido unPedido = new Pedido();
        unPedido.SetOrdenIngredientes(_posiblesOrdenes[level-1]);
        _listaPedidos.Add(unPedido);
    }

    public static Pedido crearInterpretacion(int id)
    {
        Pedido unPedido = new Pedido();
        return unPedido;
    }
    public static List<Pedido> getListaPedidos()
    {
        List<Pedido> pedidos = _listaPedidos;
        return pedidos;
    }
    //public static void ActualizarContenidoDePantalla()
    //{
    //    GenerarPedido.GenerarPedidoCanvas(pedido);
    //}
    private void GenerarPedidoCanvas(Pedido unPedido)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(prefab);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI

        panel.transform.Find("strNumeroPedido").gameObject.GetComponent<TMP_Text>().text = "Pedido # " + unPedido.GetIdPedido();

        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = "A preparar: " + unPedido.GetOrdenIngredientes();

        panel.transform.Find("strTiempoRestante").gameObject.GetComponent<TMP_Text>().text = "Tiempo Restante:";

        pedidoCreado.transform.SetParent(content.transform, false);

        iNumPedido++;
    }
    private void CrearOrdenIngredientesRandom(int level)
    {
        int cantidadIngredientes;
       
        switch (level)
        {
            case 1:
                cantidadIngredientes = RandomEntre(1, 2);
                
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:

                break;
        }

    }
    private int RandomEntre(int min, int max)
    {
        int intIndiceRandom = UnityEngine.Random.Range(min, max);
        return intIndiceRandom;
    }
    private List<String> CrearHamburguesaRandom(int level, int cantidadIngredientes)
    {
        bool hayPati=false;
        List<String> HamburguesaFinal=new List<String>();
        HamburguesaFinal.Add("Pan");
        while (hayPati==false)
        {
            for (int i = 0; i < cantidadIngredientes; i++)
            {
                HamburguesaFinal.Add(_posiblesIngredientes[level][RandomEntre(0,_posiblesIngredientes[level].Count)]);
            }
            if (HamburguesaFinal.Contains("carne")==true)
            {
                hayPati = true;
            }
        }
        HamburguesaFinal.Add("Pan");
        return HamburguesaFinal;
    }
}