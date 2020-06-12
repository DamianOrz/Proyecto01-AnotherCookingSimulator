using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform TheDest;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in player.transform)
        {
            if(child.name=="Camera")
            {
                TheDest=child.GetChild(0);
            }
        }
    }
    void OnMouseDown()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = TheDest.position;
        this.transform.parent = GameObject.Find("Destination").transform;
    }

    // Update is called once per frame
    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
