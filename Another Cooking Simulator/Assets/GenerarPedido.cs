using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GenerarPedido : MonoBehaviour
{
    public ContentSizeFitter ListaPedidos; //Obtengo el CONTENT de la pantalla a modificar (Es donde instanciaré los nuevos pedidos a mostrar)
    private float waitTime = 5.0f; //Espera 5 segundos
    private float timer = 0.0f;
    private float visualTime = 0.0f;
    private float value = 10.0f;

    int iNumPedido = 1;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
        if (timer > waitTime)
        {
            visualTime = timer;

            // Remove the recorded 2 seconds.
            timer = timer - waitTime;


            GenerarPedidoCanvas();
        }
    }

    void GenerarPedidoCanvas()
    {
        //Cuando se conecte con el boton esta funcion recibirá parámetros

        int idPedido = Random.Range(1, 4); //Me da un valor aleatorioa (1/2/3/4)
        string textoPedido = "ERROR";

        switch (idPedido)
        {
            case 1:
                textoPedido = "Hamburguesa Simple";
                break;
            case 2:
                textoPedido = "Hamburguesa Doble";
                break;
            case 3:
                textoPedido = "Hamburguesa Simple con Queso";
                break;
            case 4:
                textoPedido = "Hamburguesa Doble con Queso";
                break;
        }

        Object pedidoACrear = Resources.Load("UI/PedidoGenerado");

        GameObject pedidoCreado = Instantiate((GameObject)pedidoACrear) as GameObject;

        pedidoCreado.transform.parent = ListaPedidos.transform;
        
        pedidoCreado.transform.Find("strNumeroPedido").GetComponent<Text>().text = "Pedido #" + iNumPedido;

        pedidoCreado.transform.Find("strIngredientes").GetComponent<Text>().text = "A preparar:" + textoPedido;

        pedidoCreado.transform.Find("strTiempoRestante").GetComponent<Text>().text = "Tiempo Restante: " + "CAMBIAR CUANDO SE HAGA LA PARTE DE TIEMPO" + "segs";

        
        iNumPedido++;
        
    }
}
