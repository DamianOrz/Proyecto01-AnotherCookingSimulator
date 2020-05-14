using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaColliders : MonoBehaviour
{
    private GameObject elOtro = null;
    private Vector3 escalaCollider;
    private float alturaCollider;
    public GameObject pruebaBurgers;
    private bool yaChoco;

    // Start is called before the first frame update
    void Start()
    {
        yaChoco = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(elOtro != null)
        {
            if (elOtro.name == "BunBOT")
            {
                escalaCollider = elOtro.transform.position;
                escalaCollider.y += alturaCollider / 2;
                pruebaBurgers.transform.SetPositionAndRotation(escalaCollider, elOtro.transform.rotation);
            }
            if (elOtro.name == "BunTOP")
            {
                escalaCollider = pruebaBurgers.transform.position;
                escalaCollider.y += alturaCollider / 2;
                elOtro.transform.SetPositionAndRotation(escalaCollider, pruebaBurgers.transform.rotation);
            }
        }
    }

    void OnCollisionEnter(Collision colliderInfo)
    {
        Debug.Log(this.name + " choco con " + colliderInfo.collider.name);
        if (this.name == "Collider_Bot" && colliderInfo.gameObject.name == "BunBOT" && yaChoco == false)
        {
            escalaCollider = colliderInfo.gameObject.transform.position;
            alturaCollider = colliderInfo.gameObject.transform.localScale.y;
            alturaCollider += pruebaBurgers.transform.localScale.y;
            escalaCollider.y += alturaCollider / 2;
            Debug.Log(alturaCollider);
            pruebaBurgers.transform.SetPositionAndRotation(escalaCollider, pruebaBurgers.transform.rotation);
            pruebaBurgers.transform.parent = colliderInfo.gameObject.transform;
            elOtro = colliderInfo.gameObject;
            yaChoco = true;
        }
        if (this.name == "Collider_Top" && colliderInfo.gameObject.name == "BunTOP" && yaChoco == false)
        {
            elOtro = colliderInfo.gameObject;
            escalaCollider = pruebaBurgers.transform.position;
            alturaCollider = pruebaBurgers.transform.localScale.y;
            //alturaCollider += (((colliderInfo.gameObject.transform.localScale.y)/2)+ 0.05f);
            escalaCollider.y += alturaCollider / 2;
            Debug.Log(alturaCollider);
            elOtro.transform.SetPositionAndRotation(escalaCollider, elOtro.transform.rotation);
            elOtro.transform.parent = pruebaBurgers.transform;
            yaChoco = true;
        }
    }
}
