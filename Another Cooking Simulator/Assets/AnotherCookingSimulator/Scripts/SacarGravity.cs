using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SacarGravity : VRTK_InteractGrab
{
    // Update is called once per frame
    void Update()
    {
        if (GetGrabbableObject())
        {
            if (grabbedObject != null)
            {
                GameObject go = GetGrabbableObject();
                go.GetComponent<Rigidbody>().useGravity = true;
                go.GetComponent<Rigidbody>().isKinematic = true;    
            }
        }
    }
    
}
