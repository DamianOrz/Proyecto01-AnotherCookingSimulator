using Object = UnityEngine.Object;

public class IngredienteFactory
{
    private IngredienteConfiguration _ingredienteConfiguration;

    public IngredienteFactory(IngredienteConfiguration ingredienteConfiguration)
    {
        _ingredienteConfiguration = ingredienteConfiguration;
    }

    public Ingrediente Create(string id)
    {
        var ingrediente = _ingredienteConfiguration.getIngredientePrefabById(id);

        return Object.Instantiate(ingrediente);
    }
}
