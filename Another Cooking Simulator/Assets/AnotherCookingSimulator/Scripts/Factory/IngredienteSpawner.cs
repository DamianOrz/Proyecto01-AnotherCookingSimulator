using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredienteSpawner : MonoBehaviour
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
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            _ingredienteFactory.CreateWithPosition("carne", new Vector3(29.44f, 3.416f, -8.7951f));
        }
    }
    public void SpawnCarne()
    {
        _ingredienteFactory.CreateWithPosition("carne", _positionSpawnCarne);
    }
    public void SpawnPanSuperior()
    {
        _ingredienteFactory.CreateWithPosition("panSuperior", _positionSpawnPanSuperior);
    }
    public void SpawnPanInferior()
    {
        _ingredienteFactory.CreateWithPosition("panInferior", _positionSpawnPanInferior);
    }
    public void SpawnCheddar()
    {
        _ingredienteFactory.CreateWithPosition("cheddar", _positionSpawnCheddar);
    }

}
