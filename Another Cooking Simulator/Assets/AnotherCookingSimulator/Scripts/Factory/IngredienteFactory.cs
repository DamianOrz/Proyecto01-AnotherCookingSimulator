using Object = UnityEngine.Object;
using UnityEngine;
using Mirror;

public class IngredienteFactory : NetworkBehaviour
{
    private IngredienteConfiguration _ingredienteConfiguration;

    public IngredienteFactory(IngredienteConfiguration ingredienteConfiguration)
    {
        _ingredienteConfiguration = ingredienteConfiguration;
    }

    public Ingrediente CreateWithPosition(string id, Vector3 position)
    {
        var ingrediente = _ingredienteConfiguration.getIngredientePrefabById(id);

        return Object.Instantiate(ingrediente, position, Quaternion.identity);;
    }
    //[Server]
    //public Ingrediente SpawnIngrediente(Ingrediente ingrediente,Vector3 position)
    //{
    //    Ingrediente ingredienteASpawnear = Instantiate(ingrediente, position, Quaternion.identity);
    //    NetworkServer.Spawn(ingredienteASpawnear.gameObject);
    //    return ingredienteASpawnear;
    //}
}
