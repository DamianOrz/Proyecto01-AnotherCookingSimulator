using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaCollider2 : MonoBehaviour
{

    public GameObject anborgesa;
    public Material mat1;
    public Material mat2;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision colliderInfo)
    {
        Debug.Log(this.name);
        if(colliderInfo.collider.name == "Plane")
        {
            rend = anborgesa.GetComponent<Renderer>();
            if (this.name == "Collider_Bot")
            {
                rend.sharedMaterial = mat1;
            }
            if (this.name == "Collider_Top")
            {
                rend.sharedMaterial = mat2;
            }
        }
    }
}
