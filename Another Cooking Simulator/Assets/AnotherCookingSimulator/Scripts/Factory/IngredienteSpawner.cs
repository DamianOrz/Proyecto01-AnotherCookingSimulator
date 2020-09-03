using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteSpawner : MonoBehaviour
{
    [SerializeField] private IngredienteFactory _ingredienteFactory;

    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            _ingredienteFactory.Create("carne");
        }
        if (Input.GetKey(KeyCode.C))
        {
            _ingredienteFactory.Create("cheddar");
        }
    }
}
