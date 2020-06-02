using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCollider : MonoBehaviour
{
    [SerializeField] public GameObject hamburguesa;
    public Material[] material;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "SurfaceOfGrill")
        {
            rend = hamburguesa.GetComponent<Renderer>();
            rend.sharedMaterial = material[0];
            Debug.Log("ERROR");
        }
    }
}
