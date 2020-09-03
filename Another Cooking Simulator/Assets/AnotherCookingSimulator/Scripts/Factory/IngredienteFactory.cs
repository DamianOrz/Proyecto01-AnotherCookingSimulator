using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteFactory : MonoBehaviour
{
    [SerializeField] private Ingrediente _carne;

    public Ingrediente Create(string id)
    {
        switch (id)
        {
            case "carne":
                return Instantiate(_carne);
            default:
                throw new ArgumentOutOfRangeException($"Ingrediente with id {id} does't exist");
        }
    }
}
