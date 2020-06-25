using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AgregarNetworkTransformChilds : MonoBehaviour
{
    private VRTK_SnapDropZone snapDropZone;
    private bool noSeHizo;

    // Start is called before the first frame update
    void Start()
    {
        snapDropZone = this.GetComponentInChildren<VRTK_SnapDropZone>();
        noSeHizo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ObtenerHijos(snapDropZone).Count != 0)
        {
            if(noSeHizo)
            {
                List<VRTK_InteractableObject> hijos = ObtenerTodosLosHijos(snapDropZone);
                for(int i = 0; i < hijos.Count; i++)
                {
                    Debug.Log(hijos[i].name);
                }
                noSeHizo = false;
            }
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
