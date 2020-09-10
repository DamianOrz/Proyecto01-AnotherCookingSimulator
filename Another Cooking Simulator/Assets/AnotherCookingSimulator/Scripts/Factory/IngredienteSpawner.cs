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
        Object objeto = _ingredienteFactory.CreateWithPosition("carne", _positionSpawnCarne);
        GameObject a = ((GameObject)objeto);
        NetworkServer.Spawn(a);
    }
    public void SpawnPanSuperior()
    {
        Object objeto = _ingredienteFactory.CreateWithPosition("panSuperior", _positionSpawnPanSuperior);
        GameObject a = ((GameObject)objeto);
        NetworkServer.Spawn(a);
    }
    public void SpawnPanInferior()
    {
        Object objeto = _ingredienteFactory.CreateWithPosition("panInferior", _positionSpawnPanInferior);
        GameObject a = ((GameObject)objeto);
        NetworkServer.Spawn(a);
    }
    public void SpawnCheddar()
    {
        Object objeto = _ingredienteFactory.CreateWithPosition("cheddar", _positionSpawnCheddar);
        GameObject a = ((GameObject)objeto);
        NetworkServer.Spawn(a);
    }

}
