using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class IngredienteSpawner : NetworkBehaviour
{
    [SerializeField] private IngredienteConfiguration _ingredienteConfiguration;
    private IngredienteFactory _ingredienteFactory;

    //Position of the spawn of the ingredients
    [SerializeField] private Vector3 _positionSpawnCarne;
    [SerializeField] private Vector3 _positionSpawnPanSuperior;
    [SerializeField] private Vector3 _positionSpawnPanInferior;
    [SerializeField] private Vector3 _positionSpawnCheddar;

    private void Awake()
    {
        _ingredienteFactory = new IngredienteFactory(Instantiate(_ingredienteConfiguration));
    }
    public void SpawnCarne()
    {
        GameObject objeto = _ingredienteFactory.CreateWithPosition("carne", _positionSpawnCarne).gameObject;
        SpawnIngrediente(objeto);
    }
    public void SpawnPanSuperior()
    {
        GameObject objeto = _ingredienteFactory.CreateWithPosition("panSuperior", _positionSpawnPanSuperior).gameObject;
        SpawnIngrediente(objeto);
    }
    public void SpawnPanInferior()
    {
        GameObject objeto = _ingredienteFactory.CreateWithPosition("panInferior", _positionSpawnPanInferior).gameObject;
        SpawnIngrediente(objeto);
    }
    public void SpawnCheddar()
    {
        GameObject objeto = _ingredienteFactory.CreateWithPosition("cheddar", _positionSpawnCheddar).gameObject;
        SpawnIngrediente(objeto);
    }
    [Server]
    public void SpawnIngrediente(GameObject ingrediente)
    {
        NetworkServer.Spawn(ingrediente);
    }
}
