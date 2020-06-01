using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HasAutority : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisableOfVR;
    [SerializeField]
    Behaviour[] componentsToDisableOfPC;
    // Start is called before the first frame update
    void Start()
    {
            if(this.gameObject.name=="VRPlayer")
                for (int i = 0; i < componentsToDisableOfVR.Length; i++)
                {
                    componentsToDisableOfVR[i].enabled = false;
                }
            if(this.gameObject.name=="Player")
            {
                for (int i = 0; i < componentsToDisableOfPC.Length; i++)
                {
                    componentsToDisableOfVR[i].enabled = false;
                }       
            }
    }
    void Update()
    {

    }
}
