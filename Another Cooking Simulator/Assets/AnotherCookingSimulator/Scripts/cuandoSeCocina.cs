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
    bool touchGrill=false, estaSonando=false;
    public ParticleSystem grillSmoke;
    public GameObject burger;

    void Start()
    {
        rend = burger.GetComponent<Renderer>();
        rend.enabled = true;
    }
    // Se cuenta el tiempo que el pati esta tocando el horno
    // Y cambia estado dependiendo el tiempo que pasó
    void Update()
    {
        if (touchGrill==true)
        {
            if (estaSonando == false)
            {
                estaSonando = true;
                FindObjectOfType<AudioManager>().PlayInPosition("Fry",burger.transform.position);
            }

            if (!grillSmoke.isPlaying)
            {
              grillSmoke.Play();
            }
            
            tiempoCocinado += Time.deltaTime;
            cambiarEstado();
        }
    }
    // Cuando un pati toca el horno se cambia a true el bool::touchGrill
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "SurfaceOfGrill")
        {
            touchGrill = true;
        }
    }
    // Cuando un pati deja de tocar el horno se cambia a false el bool::touchGrill
    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "SurfaceOfGrill")
        {
            grillSmoke.Stop();
            touchGrill = false;
        }
    }

    void cambiarEstado()
    {
        if(tiempoCocinado>=12)
        {
            rend.sharedMaterial = material[1];

            var main = grillSmoke.main;
            main.startColor = new Color(0, 0, 0, 1);
        }
        else if(tiempoCocinado>=6)
        {
            rend.sharedMaterial = material[0];

            var main = grillSmoke.main;
            //main.startColor = new Color(0, 0, 23, 1);
        }
    }
}
