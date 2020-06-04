using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    private Vector3 posicion;

    // Start is called before the first frame update
    void Start()
    {
        posicion = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = posicion;
    }
}
