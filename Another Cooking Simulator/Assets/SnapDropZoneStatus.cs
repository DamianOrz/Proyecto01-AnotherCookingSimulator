namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SnapDropZoneStatus : MonoBehaviour
    {

        public VRTK_InteractableObject vrtkObject;
        public VRTK_SnapDropZone snapDropZone;

        // Start is called before the first frame update
        void Start()
        {
            snapDropZone.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(vrtkObject.IsGrabbed())
            { 
                snapDropZone.enabled = false;
            }
            else if (vrtkObject.IsGrabbed() == false)
            {
                snapDropZone.enabled = true;
            }
        }
    }
}
