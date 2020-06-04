using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class GenerarPedido : MonoBehaviour
{

    public static GameObject content;

    public static GameObject prefab;
    
    //Obtengo el CONTENT de la pantalla a modificar (Es donde instanciaré los nuevos pedidos a mostrar)
    private float waitTime = 5.0f; //Espera 5 segundos
    private float timer = 0.0f;
    private float visualTime = 0.0f;
    private float value = 10.0f;

    static int iNumPedido = 1;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;

        //// Check if we have reached beyond 2 seconds.
        //// Subtracting two is more accurate over time than resetting to zero.
        //if (timer > waitTime)
        //{
        //    visualTime = timer;

        //    // Remove the recorded 2 seconds.
        //    timer = timer - waitTime;


        //    //GenerarPedidoCanvas();
        //}
    }

    public static void GenerarPedidoCanvas(Pedido unPedido)
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros
        string textoPedido = "ERROR";
       
        GameObject pedidoCreado = Instantiate(prefab);

        GameObject panel =pedidoCreado.transform.Find("Panel").gameObject; 

        //BATALLA 1 GANADA CONTRA DAMIAN (SEÑOR FUERZAS DEL MAL), PUNTO PARA SIMI

        panel.transform.Find("strNumeroPedido").gameObject.GetComponent<TMP_Text>().text = "Pedido # " + unPedido.getId();

        panel.transform.Find("strIngredientes").gameObject.GetComponent<TMP_Text>().text = "A preparar: " + unPedido.getOrdenIngredientes();

        panel.transform.Find("strTiempoRestante").gameObject.GetComponent<TMP_Text>().text = "Tiempo Restante:";
        
        pedidoCreado.transform.SetParent(content.transform,false);

        iNumPedido++;
        
    }
}
