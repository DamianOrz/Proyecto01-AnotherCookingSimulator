using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Verificar : MonoBehaviour
{
    bool touchVerificar= false, estaSonando = false;
    public ParticleSystem grillSmoke;
    GameObject Bandeja;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchVerificar == true)
        {

        }
    }
    // Cuando un pati toca el horno se cambia a true el bool::touchGrill
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Verificacion")
        {

        }
    }
    // Cuando un pati deja de tocar el horno se cambia a false el bool::touchGrill
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Verificacion")
        {
            touchVerificar = false;
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
}
