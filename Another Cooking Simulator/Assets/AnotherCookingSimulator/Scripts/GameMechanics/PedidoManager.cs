using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class PedidoManager : MonoBehaviour
{
    public GameObject contentMostrarPedidoAlVR;
    public GameObject contentMostrarPedidoCliente;
    public GameObject contentMostrarUltimaInterpretacion;
    public static GameObject contentMostrarCliente;
    public static GameObject contentMostrarVR;
    public static GameObject contentUltimaInterpretacion;

    public GameObject prefabVR;
    public GameObject prefabClientes;
    public GameObject prefabUltimaInterpretacion;
    public static GameObject prefabVRStc;
    public static GameObject prefabClienteStc;
    public static GameObject prefabUltimaInterpretacionStc;

    static int iNumPedido = 1;
    private PedidoManager()
    {

    }

    private enum CORRECCIONES
    {
        mal = 20,
        maso =40 ,
        bien = 70,
        muyBien = 100,
    }

    private void Start()
    {
        contentMostrarVR = contentMostrarPedidoAlVR;
        contentMostrarCliente = contentMostrarPedidoCliente;
        contentUltimaInterpretacion = contentMostrarUltimaInterpretacion;

        prefabVRStc = prefabVR;
        prefabClienteStc = prefabClientes;
        prefabUltimaInterpretacionStc = prefabUltimaInterpretacion;
    }

    [SerializeField] private static List<Pedido> _listaPedidos = new List<Pedido>();

    public static void cambiarPuntaje()
    {
        int puntaje = correccion(agarrarUltimoPedido().GetOrdenIngredientes(),agarrarUltimoPedido().GetInterpretacionIngredientes());
        ScoreManager.sobreEscribir(puntaje);
        //Pregunta si ya pasaron todos los clientes
        if (DiaManager.diasInfoStc[DiaManager.diaActual].clientesEnElDia==_listaPedidos.Count)
        {
            DiaManager.FinalizarDia();
        }
    }
    public static int correccion(int[] ordenIngredientes, int[] interpretacion)
    {
        int puntos=0;
        if(ordenIngredientes.Length == interpretacion.Length)
        {
            CORRECCIONES correciones = CORRECCIONES.muyBien;
            puntos = (int)correciones;
        }
        else
        {
            CORRECCIONES correcciones = CORRECCIONES.maso;
            puntos = (int)correcciones;
        }
        return puntos;
    }
    public static void crearPedidoRandom()
    {
        int[] posiblesIngredientes=new int[DiaManager.diasInfoStc[0].posiblesIngredientes.Length];
        for (int i = 0; i < DiaManager.diasInfoStc[0].posiblesIngredientes.Length; i++)
        {
            posiblesIngredientes[i]= (int) DiaManager.diasInfoStc[DiaManager.diaActual].posiblesIngredientes[i];
        }

        Pedido unPedido = new Pedido();
        FindObjectOfType<AudioManager>().PlayInPosition("Ring", contentMostrarCliente.transform.position);
        unPedido.SetOrdenIngredientes(CrearHamburguesaRandom(posiblesIngredientes));
        MostrarPedidoDelCliente(unPedido);

        _listaPedidos.Add(unPedido);
        unPedido.SetIdPedido(_listaPedidos.Count);
    }

    public static Pedido CrearInterpretacion(int id)
    {
        Pedido unPedido;
        unPedido = agarrarUltimoPedido();
        switch (id)
        {
            //COMBO 1 = HAMBURGUESA SIMPLE
            case 1:
                int[] interpretacionSimple = new int[3] { 0, 1, 0};
                unPedido.SetInterpretacionIngredientes(interpretacionSimple);
                break;
            //COMBO 2 = HAMBURGUESA DOBLE
            case 2:
                int[] interpretacionDoble = new int[4] { 0, 1, 1, 0 };
                unPedido.SetInterpretacionIngredientes(interpretacionDoble);
                break;
            //COMBO 3 = HAMBURGUESA CON QUESO
            case 3:
                int[] interpretacionConQueso = new int[4] { 0, 1, 2, 0 };
                unPedido.SetInterpretacionIngredientes(interpretacionConQueso);
                break;
            default:
                break;
        }
        MostrarPedidoAlDeVR(unPedido);
        return unPedido;
    }
    public static List<Pedido> getListaPedidos()
    {
        List<Pedido> pedidos = _listaPedidos;
        return pedidos;
    }
    public static void LimpiarListaPedidos()
    {
        _listaPedidos.Clear();
    }
    public static Pedido agarrarUltimoPedido()
    {
        Pedido Pedido;
        int indice = _listaPedidos.Count-1;
        Pedido = _listaPedidos[indice];
        return Pedido;
    }
    public static void MostrarPedidoAlDeVR(Pedido unPedido)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(prefabVRStc);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI

        panel.transform.Find("strNumeroPedido").gameObject.GetComponent<TMP_Text>().text = "Pedido # " + unPedido.GetIdPedido();

        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = "A preparar: " + CambiarArrayAString(unPedido.GetInterpretacionIngredientes());

        panel.transform.Find("strTiempoRestante").gameObject.GetComponent<TMP_Text>().text = "Tiempo Restante:";

        pedidoCreado.transform.SetParent(contentMostrarVR.transform, false);

        iNumPedido++;
    }
    public static void MostrarPedidoDelCliente(Pedido unPedido)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(prefabClienteStc);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI
        panel.transform.Find("strConsumibles").gameObject.GetComponent<TMP_Text>().text = "" + CambiarArrayAString(unPedido.GetOrdenIngredientes());

        pedidoCreado.transform.SetParent(contentMostrarCliente.transform, false);

        iNumPedido++;
    }
    public static void MostrarVerificacion(int[] Ingredientes)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(prefabUltimaInterpretacionStc);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI
        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = CambiarArrayAString(Ingredientes);

        pedidoCreado.transform.SetParent(contentUltimaInterpretacion.transform, false);

        iNumPedido++;
    }
    public static string CambiarArrayAString(int[] listaIngredientes)
    {
        if (DiaManager.diaActual==1)
        {

        }
        string Hamburguesa="";
        foreach (int ingrediente in listaIngredientes)
        {
            Hamburguesa += DiaManager.diasInfoStc[DiaManager.diaActual].posiblesIngredientes[ingrediente].ToString() + " ";
        }
        return Hamburguesa;
    }
    private static int RandomEntre(int minInclusive, int maxInclusive)
    {
        int intIndiceRandom = UnityEngine.Random.Range(minInclusive, maxInclusive + 1);
        return intIndiceRandom;
    }
    private static int[] CrearHamburguesaRandom(int[] posiblesIngredientes)
    {
        int maxIngredientesEntrePanes = DiaManager.diasInfoStc[DiaManager.diaActual].maxIngredientesEntrePanes;
        int ingredientesEntrePanes = RandomEntre(1, maxIngredientesEntrePanes);
        if (ingredientesEntrePanes==2)
        {

        }
        int[] vector = new int[ingredientesEntrePanes+2];

        vector[0] = posiblesIngredientes[0];
        vector[vector.Length - 1] = posiblesIngredientes[0];
        vector[RandomEntre(1, ingredientesEntrePanes)] = posiblesIngredientes[1];
        for (int i = 1; i < vector.Length-1; i++)
        {
            if (vector[i] == 0)
            {
                vector[i] = RandomEntre(1, posiblesIngredientes.Length-1);
            }
        }
        return vector;
    }

}