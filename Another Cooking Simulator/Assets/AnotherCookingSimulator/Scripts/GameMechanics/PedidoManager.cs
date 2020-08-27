using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mirror;


public class PedidoManager : NetworkBehaviour
{
    public static PedidoManager instancePedidoManager;

    public GameObject contentMostrarPedidoAlVR;
    public GameObject contentMostrarPedidoCliente;
    public GameObject contentMostrarUltimaInterpretacion;

    private NetworkIdentity objNetId;

    public GameObject prefabVR;
    public GameObject prefabClientes;
    public GameObject prefabUltimaInterpretacion;

    static int iNumPedido = 1;

    private enum CORRECCIONES
    {
        mal = 20,
        maso = 40,
        bien = 70,
        muyBien = 100,
    }

    private void Awake()
    {
        if (instancePedidoManager != null && instancePedidoManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instancePedidoManager = this;
        }
    }

    [SerializeField] private static List<Pedido> _listaPedidos = new List<Pedido>();

    public void cambiarPuntaje()
    {
        int puntaje = correccion(agarrarUltimoPedido().GetOrdenIngredientes(), agarrarUltimoPedido().GetInterpretacionIngredientes());
        ScoreManager.sobreEscribir(puntaje);
        //Pregunta si ya pasaron todos los clientes
        if (DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia == _listaPedidos.Count)
        {
            DiaManager.instanceDiaManager.FinalizarDia();
        }
    }
    public int correccion(int[] ordenIngredientes, int[] interpretacion)
    {
        int puntos = 0;
        if (ordenIngredientes.Length == interpretacion.Length)
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

    [Server]
    public void crearPedidoRandom()
    {
        int[] posiblesIngredientes = new int[DiaManager.instanceDiaManager.diasInfo[0].posiblesIngredientes.Length];
        for (int i = 0; i < DiaManager.instanceDiaManager.diasInfo[0].posiblesIngredientes.Length; i++)
        {
            posiblesIngredientes[i] = (int)DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].posiblesIngredientes[i];
        }

        Pedido unPedido = new Pedido();
        FindObjectOfType<AudioManager>().Play("FX-Ring");
        unPedido.SetOrdenIngredientes(CrearHamburguesaRandom(posiblesIngredientes));
        MostrarPedidoDelCliente(unPedido);

        _listaPedidos.Add(unPedido);
        unPedido.SetIdPedido(_listaPedidos.Count);
    } //Genera un pedido de forma aleatoria

    public Pedido CrearInterpretacion(int id)
    {
        Pedido unPedido;
        unPedido = agarrarUltimoPedido();
        switch (id)
        {
            //COMBO 1 = HAMBURGUESA SIMPLE
            case 1:
                int[] interpretacionSimple = new int[3] { 0, 1, 0 };
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
    public List<Pedido> getListaPedidos() { return _listaPedidos; } //Devuelve la lista de pedidos
    public void LimpiarListaPedidos()
    {
        _listaPedidos.Clear();
    }
    public Pedido agarrarUltimoPedido()
    {
        Pedido Pedido;
        int indice = _listaPedidos.Count - 1;
        Pedido = _listaPedidos[indice];
        return Pedido;
    }

    public void MostrarPedidoAlDeVR(Pedido unPedido)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(instancePedidoManager.prefabVR);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI

        panel.transform.Find("strNumeroPedido").gameObject.GetComponent<TMP_Text>().text = "Pedido # " + unPedido.GetIdPedido();

        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = "A preparar: " + CambiarArrayAString(unPedido.GetInterpretacionIngredientes());

        panel.transform.Find("strTiempoRestante").gameObject.GetComponent<TMP_Text>().text = "Tiempo Restante:";

        pedidoCreado.transform.SetParent(instancePedidoManager.contentMostrarPedidoAlVR.transform, false);

        iNumPedido++;
    }

    [Server]
    public void MostrarPedidoDelCliente(Pedido unPedido)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(instancePedidoManager.prefabClientes);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;
        panel.transform.Find("strConsumibles").gameObject.GetComponent<TMP_Text>().text = "" + CambiarArrayAString(unPedido.GetOrdenIngredientes());
        pedidoCreado.transform.SetParent(instancePedidoManager.contentMostrarPedidoCliente.transform, false);

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI
        NetworkServer.Spawn(pedidoCreado);

        RpcPonerComoHijoPanel(unPedido);

        //RpcPonerComoHijoPanel(pedidoCreado);
        iNumPedido++;
    }

    [ClientRpc]
    public void RpcPonerComoHijoPanel(Pedido unPedido)
    {
        
    }
    public void MostrarVerificacion(int[] Ingredientes)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(instancePedidoManager.prefabUltimaInterpretacion);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI
        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = CambiarArrayAString(Ingredientes);

        pedidoCreado.transform.SetParent(instancePedidoManager.contentMostrarUltimaInterpretacion.transform, false);

        iNumPedido++;
    }

    public string CambiarArrayAString(int[] listaIngredientes)
    {
        if (DiaManager.instanceDiaManager.diaActual == 1)
        {

        }
        string Hamburguesa = "";
        foreach (int ingrediente in listaIngredientes)
        {
            Hamburguesa += DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].posiblesIngredientes[ingrediente].ToString() + " ";
        }
        return Hamburguesa;
    }
    private int RandomEntre(int minInclusive, int maxInclusive)
    {
        int intIndiceRandom = UnityEngine.Random.Range(minInclusive, maxInclusive + 1);
        return intIndiceRandom;
    }
    private int[] CrearHamburguesaRandom(int[] posiblesIngredientes)
    {
        int maxIngredientesEntrePanes = DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].maxIngredientesEntrePanes;
        int ingredientesEntrePanes = RandomEntre(1, maxIngredientesEntrePanes);
        if (ingredientesEntrePanes == 2)
        {

        }
        int[] vector = new int[ingredientesEntrePanes + 2];

        vector[0] = posiblesIngredientes[0];
        vector[vector.Length - 1] = posiblesIngredientes[0];
        vector[RandomEntre(1, ingredientesEntrePanes)] = posiblesIngredientes[1];
        for (int i = 1; i < vector.Length - 1; i++)
        {
            if (vector[i] == 0)
            {
                vector[i] = RandomEntre(1, posiblesIngredientes.Length - 1);
            }
        }
        return vector;
    }
}