using Object = UnityEngine.Object;
using UnityEngine;
public class IngredienteFactory
{
    private IngredienteConfiguration _ingredienteConfiguration;

    public IngredienteFactory(IngredienteConfiguration ingredienteConfiguration)
    {
        _ingredienteConfiguration = ingredienteConfiguration;
    }

    public Ingrediente CreateWithPosition(string id, Vector3 position)
    {
        var ingrediente = _ingredienteConfiguration.getIngredientePrefabById(id);

        return Object.Instantiate(ingrediente, position, Quaternion.identity);
    }
}
