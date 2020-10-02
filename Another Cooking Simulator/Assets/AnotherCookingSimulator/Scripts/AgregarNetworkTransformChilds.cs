using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Mirror;

public class AgregarNetworkTransformChilds : NetworkBehaviour
{
    private VRTK_SnapDropZone snapDropZone;

    // Start is called before the first frame update
    void Start()
    {
        snapDropZone = this.GetComponentInChildren<VRTK_SnapDropZone>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.name=="Bandeja3" && snapDropZone.GetCurrentSnappedObject() != null)
        {
            GameObject currentGameObject = snapDropZone.GetCurrentSnappedObject();
            NetworkTransformChild ntc = this.gameObject.AddComponent<NetworkTransformChild>();
            ntc.target = currentGameObject.GetComponent<Transform>();
            ntc.setClieltAuthority();
            snapDropZone = currentGameObject.GetComponentInChildren<VRTK_SnapDropZone>();
        }
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
            }else
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

    private VRTK_InteractableObject obtenerHijoDeSnapDropZone(VRTK_SnapDropZone objeto)
    {
        VRTK_InteractableObject ingrediente = null;
        foreach (Transform child in objeto.transform)
        {
            if (child.name != "HighlightContainer")
            {
                ingrediente = child.GetComponent<VRTK_InteractableObject>();
            }
        }
        
        return ingrediente;
    }
}
