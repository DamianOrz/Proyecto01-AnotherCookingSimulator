using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteFactory : MonoBehaviour
{
    [SerializeField] private Ingrediente _carne;
    [SerializeField] private Ingrediente _cheddar;

    [SerializeField] private Ingrediente[] _ingredientes;
    private Dictionary<string, Ingrediente> _idToIngrediente;

    private void Awake()
    {
        _idToIngrediente = new Dictionary<string, Ingrediente>();
        foreach (var ingrediente in _ingredientes)
        {
            _idToIngrediente.Add(ingrediente.Id, ingrediente);
        }
    }
    public Ingrediente Create(string id)
    {
        _idToIngrediente.TryGetValue(id, out var ingrediente);
        return Instantiate(ingrediente);
    }
}
