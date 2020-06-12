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

    private enum _posiblesIngredientesDia1
    {
        carne=1,
        queso=2
    }
    private enum _posiblesIngredientesDia2
    {
        carne = 1,
        queso = 2,
        panceta = 3,
        cebolla=4
    }
    private enum _posiblesIngredientesDia3
    {
        carne = 1,
        queso = 2,
        panceta = 3,
        cebolla = 4,
        lechuga = 5
    }
    private enum _posiblesIngredientesDia4
    {
        carne = 1,
        queso = 2,
        panceta = 3,
        cebolla = 4,
        lechuga = 5,
        tomate = 6
    }
    private enum _posiblesIngredientesDia5
    {
        carne = 1,
        queso = 2,
        panceta = 3,
        cebolla = 4,
        lechuga = 5,
        tomate = 6,
        ketchup=7,
        mayonesa=8
    }

    private enum _correcciones
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
    private static List<String> _hamburguesasDelCliente = new List<String>(){"Hamburguesa Simple","Hamburguesa Doble","Hamburguesa con queso","Hamburguesa doble con queso"};
    private static List<String>[] _combosHamburguesas = new List<String>[]
    {
    new List<String>() { "pan","carne","pan"} ,
    new List<String>() { "pan", "carne","carne", "pan" },
    new List<String>() { "pan", "queso", "carne","pan"},
    new List<String>() { "pan", "carne", "queso", "carne","pan"}
    };

    public static int obtenerPuntaje()
    {
        int puntaje=0;
        puntaje = corregir(agarrarUltimoPedido().GetOrdenIngredientes(),agarrarUltimoPedido().GetInterpretacionIngredientes());
        return puntaje;
    }
    public static int corregir(List<String> ordenIngredientes, List<String> interpretacion)
    {
        int puntos=0;
        if(ordenIngredientes.Count == interpretacion.Count)
        {
            _correcciones correciones = _correcciones.muyBien;
            puntos = (int)correciones;
        }
        else
        {
            _correcciones correcciones = _correcciones.maso;
            puntos = (int)correcciones;
        }
        return puntos;
    }
    public static void crearPedidoRandom(int level)
    {
        int IndiceRandom = UnityEngine.Random.Range(0, 3);
        Pedido unPedido = new Pedido();
        unPedido.SetOrdenIngredientes(_combosHamburguesas[IndiceRandom]);
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
                unPedido.SetInterpretacionIngredientes(_combosHamburguesas[id - 1]);
                break;
            //COMBO 2 = HAMBURGUESA DOBLE
            case 2:
                unPedido.SetInterpretacionIngredientes(_combosHamburguesas[id - 1]);
                break;
            //COMBO 3 = HAMBURGUESA CON QUESO
            case 3:
                unPedido.SetInterpretacionIngredientes(_combosHamburguesas[id - 1]);
                break;
            //COMBO 4 = HAMBURGUESA DOBLE CON QUESO
            case 4:
                unPedido.SetInterpretacionIngredientes(_combosHamburguesas[id - 1]);
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

        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = "A preparar: " + CambiarListaAString(unPedido.GetInterpretacionIngredientes());

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
        panel.transform.Find("strConsumibles").gameObject.GetComponent<TMP_Text>().text = "" + CambiarListaAString(unPedido.GetOrdenIngredientes());

        pedidoCreado.transform.SetParent(contentMostrarCliente.transform, false);

        iNumPedido++;
    }
    public static void MostrarUltimaInterpretacion(String Ingredientes)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";

        GameObject pedidoCreado = Instantiate(prefabUltimaInterpretacionStc);

        GameObject panel = pedidoCreado.transform.Find("Panel").gameObject;

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI
        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = Ingredientes;

        pedidoCreado.transform.SetParent(contentUltimaInterpretacion.transform, false);

        iNumPedido++;
    }
    public static string CambiarListaAString(List<String> listaIngredientes)
    {
        string Hamburguesa="";
        foreach (String ingrediente in listaIngredientes)
        {
            Hamburguesa += ingrediente+" ";
        }
        return Hamburguesa;
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