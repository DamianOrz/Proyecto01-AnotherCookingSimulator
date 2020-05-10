using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cuandoSeCocina : MonoBehaviour
{
    public float tiempoCocinado = 0;
    //Permito ingresar materiales para el cambio de estado
    public Material[] material;
    Renderer rend;
    bool touchGrill=false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    // Se cuenta el tiempo que el pati esta tocando el horno
    // Y cambia estado dependiendo el tiempo que pasó
    void Update()
    {
        if(touchGrill==true)
        {
            FindObjectOfType<AudioManager>().Play("Fry");
            tiempoCocinado += Time.deltaTime;
            cambiarEstado();
        }
    }
    // Cuando un pati toca el horno se cambia a true el bool::touchGrill
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "SurfaceOfGrill")
        {
            touchGrill= true;
        }
    }
    // Cuando un pati deja de tocar el horno se cambia a false el bool::touchGrill
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "SurfaceOfGrill")
        {
            touchGrill = false;
        }
    }

    void cambiarEstado()
    {
        if(tiempoCocinado>=12)
        {
            rend.sharedMaterial = material[1];
        }else if(tiempoCocinado>=6)
        {
            rend.sharedMaterial = material[0];
        }
    }
    
}
