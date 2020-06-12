using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar : MonoBehaviour
{
    public float tiempoCocinado = 0;
    //Permito ingresar materiales para el cambio de estado
    public Material[] material;
    Renderer rend;
    bool touchGrill = false, estaSonando = false;
    public ParticleSystem grillSmoke;
    public GameObject burger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
