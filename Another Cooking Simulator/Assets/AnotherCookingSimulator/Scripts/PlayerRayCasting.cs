using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCasting : MonoBehaviour
{
    [Range(0.1f,15f)]
    public float distanceToSee;
    RaycastHit whatIHit;

    Vector3 position = new Vector3(-6.13f, 1.937f, -5.388f);

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    [SerializeField] Pedido pedido = new Pedido();
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);
        if(Physics.Raycast(this.transform.position,this.transform.forward, out whatIHit,distanceToSee))
        {
            if(Input.GetKeyDown(KeyCode.E) && whatIHit.collider.gameObject.tag== "BtnCrearPedidoRandom")
            {
                Debug.Log("Se crea el pedido random");
                FindObjectOfType<AudioManager>().PlayInPosition("ButtonClick", whatIHit.collider.gameObject.transform.position);
                PedidoManager.crearPedidoRandom();
                List<Pedido> pedidos = PedidoManager.getListaPedidos();
            }
        }
    }
}
