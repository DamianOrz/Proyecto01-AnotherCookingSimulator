using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class DisableComponents : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    void Start()
    {
        if (isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
    }
}
