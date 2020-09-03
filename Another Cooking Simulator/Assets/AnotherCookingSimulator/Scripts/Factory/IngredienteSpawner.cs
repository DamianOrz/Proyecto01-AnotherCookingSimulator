using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteSpawner : MonoBehaviour
{
    [SerializeField] private IngredienteConfiguration _ingredienteConfiguration;
    private IngredienteFactory _ingredienteFactory;

    private void Awake()
    {
        _ingredienteFactory = new IngredienteFactory(Instantiate(_ingredienteConfiguration));
    }
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
