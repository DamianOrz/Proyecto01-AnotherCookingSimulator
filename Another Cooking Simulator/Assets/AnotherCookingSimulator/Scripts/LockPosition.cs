using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    private Vector3 posicion;
    private Quaternion rotacion;

    // Start is called before the first frame update
    void Start()
    {
        posicion = this.transform.localPosition;
        rotacion = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = posicion;
        this.transform.localRotation = rotacion;
    }
}
