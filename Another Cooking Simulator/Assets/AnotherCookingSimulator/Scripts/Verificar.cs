using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Verificar : MonoBehaviour
{
    public static Verificar instanceVerificar;
    GameObject Bandeja;
    private bool noSeHizo;

    private void Awake()
    {
        if (instanceVerificar != null && instanceVerificar != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanceVerificar = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        noSeHizo = true;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    //void OnCollisionEnter(Collision collisionInfo)
    //{
    //    if (collisionInfo.collider.tag == "Verificacion")
    //    {
            
    //        VRTK_SnapDropZone snapDropZone = this.GetComponentInChildren<VRTK_SnapDropZone>();
            
    //    }
    //}
    public void Evaluar(GameObject pedidoAVerificar, int indiceEnListaPedidos)
    {
        noSeHizo = true;
        VRTK_SnapDropZone snapDropZone = pedidoAVerificar.GetComponentInChildren<VRTK_SnapDropZone>();
        if (ObtenerHijos(snapDropZone).Count != 0)
        {
            if (noSeHizo)
            {
                List<VRTK_InteractableObject> hijos = ObtenerTodosLosHijos(snapDropZone);
                List<string> listaNombres = new List<string>();
                noSeHizo = false;
                string strIngredientes = "";
                for(int i = 0; i < hijos.Count; i++)
                {
                    strIngredientes += hijos[i].name + " ";
                    listaNombres.Add(hijos[i].name);
                }
                int[] interpretacionDeVR = new int[listaNombres.Count];
                for (int i = 0; i < listaNombres.Count; i++)
                {
                    if (listaNombres[i].Contains("Pan"))
                    {
                        interpretacionDeVR[i] = 0;
                    }else if (listaNombres[i].Contains("Paty"))
                    {
                        interpretacionDeVR[i] = 1;
                    }else if (listaNombres[i].Contains("Cheddar"))
                    {
                        interpretacionDeVR[i] = 2;
                    }else if (listaNombres[i].Contains("Cebolla"))
                    {
                        interpretacionDeVR[i] = 3;
                    }else if (listaNombres[i].Contains("Bacon"))
                    {
                        interpretacionDeVR[i] = 4;
                    }
                }
                //PedidoManager.instancePedidoManager.agarrarUltimoPedido().SetInterpretacionIngredientes(interpretacionDeVR);
                ClientesManager.instanceClientesManager.seEntregoUnPedido();//Se avisa que ya se entrego un pedido al cliente manager
                //PedidoManager.instancePedidoManager.MostrarVerificacion(interpretacionDeVR);//Mostrar en el canvas
                PedidoManager.instancePedidoManager.cambiarPuntaje(indiceEnListaPedidos, interpretacionDeVR);
                    
                    
                strIngredientes = "";
            }
        }
    }
    List<GameObject> listaHamburguesa(GameObject go)
    {
        List<GameObject> listaHamburguesa = new List<GameObject>();
        foreach (Transform child in go.transform)
        {
            listaHamburguesa.Add(child.GetComponent<GameObject>());
        }
        return listaHamburguesa;
    }
    private List<VRTK_InteractableObject> ObtenerTodosLosHijos(VRTK_SnapDropZone snapDropZone)
    {
        List<VRTK_InteractableObject> hijos = new List<VRTK_InteractableObject>();
        VRTK_SnapDropZone objectParaBuscarHijos = snapDropZone;
        bool tieneHijos = true;
        do
        {
            if (ObtenerHijos(objectParaBuscarHijos).Count != 0)
            {
                hijos.Add(ObtenerHijos(objectParaBuscarHijos)[0]);
                VRTK_SnapDropZone nuevoObject = ObtenerHijos(objectParaBuscarHijos)[0].GetComponentInChildren<VRTK_SnapDropZone>();
                objectParaBuscarHijos = nuevoObject;
            }
            else
            {
                tieneHijos = false;
            }
        } while (tieneHijos == true);
        return hijos;
    }
    private List<VRTK_InteractableObject> ObtenerHijos(VRTK_SnapDropZone objeto)
    {
        List<VRTK_InteractableObject> hijos = new List<VRTK_InteractableObject>();
        foreach (Transform child in objeto.transform)
        {
            if (child.name != "HighlightContainer")
            {
                hijos.Add(child.GetComponent<VRTK_InteractableObject>());
            }
        }
        return hijos;
    }
}
