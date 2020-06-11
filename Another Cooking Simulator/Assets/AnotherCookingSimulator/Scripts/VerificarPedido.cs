using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VerificarPedido : MonoBehaviour
{
    private bool noSeHizo;
    private string strIngredientes;
    // Start is called before the first frame update
    void Start()
    {
        noSeHizo = true;
    }

    // Update is called once per frame
    void Update()
    {
        VRTK_SnapDropZone snapDropZone = this.GetComponent<VRTK_SnapDropZone>();
        if (ObtenerHijos(snapDropZone).Count != 0)
        {
            if (noSeHizo)
            {
                List<VRTK_InteractableObject> hijos = ObtenerTodosLosHijos();
                noSeHizo = false;
                
                for (int i = 0; i < hijos.Count; i++)
                {
                    strIngredientes += hijos[i].name+" ";
                }
                PedidoManager.MostrarUltimaInterpretacion(strIngredientes);
                strIngredientes = "";
            }
        }
        if (noSeHizo == false && ObtenerHijos(snapDropZone).Count == 0)
        {
            noSeHizo = true;
        }
    }

    private List<VRTK_InteractableObject> ObtenerTodosLosHijos()
    {
        List<VRTK_InteractableObject> hijos = new List<VRTK_InteractableObject>();
        VRTK_SnapDropZone objectParaBuscarHijos = this.GetComponent<VRTK_SnapDropZone>();
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
