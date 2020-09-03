using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredienteConfiguration", menuName = "ScriptableObjects/IngredienteConfiguration", order = 1)]
public class IngredienteConfiguration : ScriptableObject
{
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
    public Ingrediente getIngredientePrefabById(string id)
    {
        _idToIngrediente.TryGetValue(id, out var ingrediente);
        return ingrediente;
    }
}
