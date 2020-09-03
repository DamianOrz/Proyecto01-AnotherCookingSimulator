using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteSpawner : MonoBehaviour
{
    [SerializeField] private IngredienteFactory _ingredienteFactory;

    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            _ingredienteFactory.Create("carne");
        }
    }
}
