using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaColliders : MonoBehaviour
{
    public GameObject anborguesa;
    private GameObject bun = null;
    private Vector3 escalaCollider;
    private Renderer rend;
    private FixedJoint fj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bun != null)
        {
            escalaCollider = bun.transform.position;
            escalaCollider.y += bun.transform.position.y;
            anborguesa.transform.SetPositionAndRotation(escalaCollider,anborguesa.transform.rotation);
        }
    }

    void OnCollisionEnter(Collision colliderInfo)
    {
        if(this.name == "Collider_Bot" && colliderInfo.collider.name == "BunTOP")
        {
            bun = colliderInfo.gameObject;
            escalaCollider = bun.transform.position;
            escalaCollider.y += bun.transform.position.y;
            anborguesa.transform.SetPositionAndRotation(escalaCollider, anborguesa.transform.rotation);
            anborguesa.transform.parent = bun.transform;
            fj = bun.GetComponent<FixedJoint>();
            fj.connectedBody = anborguesa.GetComponent<Rigidbody>();
        }
    }
}
