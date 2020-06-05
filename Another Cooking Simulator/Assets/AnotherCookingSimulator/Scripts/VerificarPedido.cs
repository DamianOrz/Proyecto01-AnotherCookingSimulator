using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VerificarPedido : MonoBehaviour
{
    private bool yaLoHizo = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        VRTK_SnapDropZone snapDropZone = this.GetComponent<VRTK_SnapDropZone>();
        List<VRTK_InteractableObject> hijos = new List<VRTK_InteractableObject>();
        VRTK_SnapDropZone objectParaBuscarHijos;
        if(snapDropZone.GetCurrentSnappedObject() != null /*&& this.GetComponentInParent<VRTK_InteractableObject>().IsGrabbed() == true*/)
        {
            objectParaBuscarHijos = this.GetComponent<VRTK_SnapDropZone>();
            bool tieneHijos = true;
            do
            {
                if(ObtenerHijos(objectParaBuscarHijos).Count != 0)
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
            for (int i = 0; i < hijos.Count; i++)
            {
                Debug.Log(hijos[i].name);
            }
            yaLoHizo = true;
        }
    }

    private List<VRTK_InteractableObject> ObtenerHijos(VRTK_SnapDropZone objeto)
    {
        List<VRTK_InteractableObject> hijos = new List<VRTK_InteractableObject>();
        foreach(Transform child in objeto.transform)
        {
            if(child.name != "HighlightContainer")
            {
                hijos.Add(child.GetComponent<VRTK_InteractableObject>());
            }
        }
        return hijos;
    }
}
