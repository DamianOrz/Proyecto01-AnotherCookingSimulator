using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mirror;
using Random = System.Random;

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
    private List<int> _listaDeMesasDisponibles = new List<int>() {1, 2, 3, 4, 5, 6};
    public void cambiarPuntaje(int indice, int[] interpretacionVR)
    {
        int puntaje = correccion(_listaPedidos[indice].GetOrdenIngredientes(), interpretacionVR); //Correccion
        
        ScoreManager.sobreEscribir(puntaje); //Sumar el score
        int[] idImages = new int[1] { 1 };
        string[] dialogos = new string[1] { "Obtuviste " + puntaje + " puntos" };
        DialogoManager.instanceDialogoInformacion.hacerDialogo(idImages, dialogos.Length, dialogos); // Mostrar dialogo
        _listaDeMesasDisponibles.Add(_listaPedidos[indice].GetNumMesa()); //Añadimos la mesa a la lista de mesas disponibles
        _listaPedidos.RemoveAt(indice); //Sacar pedido de la lista de pedidos
        //Pregunta si ya pasaron todos los clientes
        if (DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].clientesEnElDia == _listaPedidos.Count)
        {
            //DiaManager.instanceDiaManager.FinalizarDia();
        }
    }
    private int correccion(int[] ordenIngredientes, int[] interpretacion)
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

    public void crearPedidoRandom()
    {
        if (_listaDeMesasDisponibles.Count <= 0) return;
        int[] posiblesIngredientes = new int[DiaManager.instanceDiaManager.diasInfo[0].posiblesIngredientes.Length];

        for (int i = 0; i < DiaManager.instanceDiaManager.diasInfo[0].posiblesIngredientes.Length; i++)
        {
            posiblesIngredientes[i] = (int)DiaManager.instanceDiaManager.diasInfo[DiaManager.instanceDiaManager.diaActual].posiblesIngredientes[i];
        }

        Pedido unPedido = new Pedido();
        FindObjectOfType<AudioManager>().Play("FX-Ring");
        unPedido.SetOrdenIngredientes(CrearHamburguesaRandom(posiblesIngredientes));
        unPedido.SetNumMesa(IdMesaDisponible(_listaDeMesasDisponibles));
        MostrarPedidoDelCliente(unPedido);

        _listaPedidos.Add(unPedido);
        unPedido.SetIdPedido(_listaPedidos.Count);
    } //Genera un pedido de forma aleatoria
    private int IdMesaDisponible(List<int> mesasDisponibles)
    {
        int index = RandomEntre(0,_listaDeMesasDisponibles.Count-1);
        int numeroDeMesa = mesasDisponibles[index];
        mesasDisponibles.RemoveAt(index);
        return numeroDeMesa;
    }
    public Pedido CrearInterpretacion(int[] interpretacion)
    {
        Pedido unPedido;
        unPedido = agarrarUltimoPedido();
        unPedido.SetInterpretacionIngredientes(interpretacion);
        MostrarPedidoAlDeVR(unPedido);
        return unPedido;
    }
    private List<Pedido> getListaPedidos() { return _listaPedidos; } //Devuelve la lista de pedidos
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
        //Agarro orden de ingredientes
        string strOrden = CambiarArrayAString(unPedido.GetInterpretacionIngredientes());
        //Ejecuto funcion server y le mando los ingredientes
        SincronizarPedidoEnPantalla(unPedido.GetIdPedido(), strOrden, unPedido.GetNumMesa());

        iNumPedido++;
    }

    private void MostrarPedidoDelCliente(Pedido unPedido)
    {
        //Agarro orden de ingredientes
        string strOrden = CambiarArrayAString(unPedido.GetOrdenIngredientes());
        
        //Ejecuto funcion server y le mando los ingredientes
        SincronizarPedidoEnPantalla(-1, strOrden, unPedido.GetNumMesa());

        iNumPedido++;
    }

    public void MostrarVerificacion(int[] Ingredientes)
    {
        //Agarro orden de ingredientes
        string strOrden = CambiarArrayAString(Ingredientes);
        //Ejecuto funcion server y le mando los ingredientes
        SincronizarPedidoEnPantalla(-2, strOrden, -1);

        iNumPedido++;
    }

    #region OnlinePedidos
    [Server]
    private void SincronizarPedidoEnPantalla(int idPedido, string ordenDeIngredientes, int numMesa)
    {
        //Ejecuto un rpc para ejecutar el codigo en todos los clientes(mando orden)
        if (idPedido == -1)
        {
            RpcMostrarPedidoACadaCliente(ordenDeIngredientes, numMesa);
            return;
        }
        else if (idPedido == -2)
        {
            RpcMostrarVerificacion(ordenDeIngredientes);
            return;
        }
        else
        {
            RpcMostrarPedidoAVR(idPedido, ordenDeIngredientes);
        }
    }
    [ClientRpc]
    private void RpcMostrarPedidoACadaCliente(string ordenIngredientes, int numMesa)
    {
        //Instancio el prefab del cliente
        GameObject pedidoCreado = Instantiate(instancePedidoManager.prefabClientes);
        //Busco el panel
        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;
        //Le inserto el orden de ingredientes al prefab

        Debug.Log("SIMON: Los ingrediente son :"+ ordenIngredientes);
        panel.transform.Find("strConsumibles").gameObject.GetComponent<TMP_Text>().text = ordenIngredientes;
        panel.transform.Find("strNumMesa").gameObject.GetComponent<TMP_Text>().text = "Numero de mesa: " + numMesa;
        //Lo hago hijo de la pantalla para que se vea
        pedidoCreado.transform.SetParent(instancePedidoManager.contentMostrarPedidoCliente.transform, false);
    }
    [ClientRpc]
    private void RpcMostrarVerificacion(string ordenIngredientes)
    {
        //Instancio el prefab del cliente
        GameObject pedidoCreado = Instantiate(instancePedidoManager.prefabUltimaInterpretacion);
        //Busco el panel
        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;
        //Le inserto el orden de ingredientes al prefab
        panel.transform.Find("strConsumibles").gameObject.GetComponent<TMP_Text>().text = ordenIngredientes;
        //Lo hago hijo de la pantalla para que se vea
        pedidoCreado.transform.SetParent(instancePedidoManager.contentMostrarUltimaInterpretacion.transform, false);
    }
    [ClientRpc]
    private void RpcMostrarPedidoAVR(int idPedido, string ordenIngredientes)
    {
        //Instancio el prefab del cliente
        GameObject pedidoCreado = Instantiate(instancePedidoManager.prefabVR);
        //Busco el panel
        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;
        //Le inserto la informacion
        panel.transform.Find("strNumeroPedido").gameObject.GetComponent<TMP_Text>().text = "Pedido # " + idPedido;

        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = "A preparar: " + ordenIngredientes;

        panel.transform.Find("strTiempoRestante").gameObject.GetComponent<TMP_Text>().text = "Tiempo Restante:";
        //Lo hago hijo de la pantalla del de VR para que se vea
        pedidoCreado.transform.SetParent(instancePedidoManager.contentMostrarPedidoAlVR.transform, false);
    }
    #endregion

    private string CambiarArrayAString(int[] listaIngredientes)
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

    public void EvaluarPedido(int idMesa, GameObject pedido)
    {
        int indice= BuscarPedidoEnEstaMesa(idMesa);
        if(indice == -1)
        {
            int[] idImages = new int[1] { 0};
            string[] dialogos = new string[1] { "No hay ningun pedido en esta mesa"};
            DialogoManager.instanceDialogoInformacion.hacerDialogo(idImages, dialogos.Length, dialogos);
            return;
        } //No se encontro un pedido con ese id de mesa
        Pedido unPedido = _listaPedidos[indice]; //Pedido con el id de la mesa que se encontro
        Verificar.instanceVerificar.Evaluar(pedido, indice);
    }

    private int BuscarPedidoEnEstaMesa(int idMesa)
    {
        int i = 0;
        while (i<_listaPedidos.Count)
        {
            if (_listaPedidos[i].GetNumMesa() == idMesa) return i;
            i++;
        }
        return i = -1;
    }
}