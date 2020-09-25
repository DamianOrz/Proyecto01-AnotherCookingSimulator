using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TomarPedido : MonoBehaviour
{
    private List<int> _interpretacionDePC = new List<int>();

    //Mostrar el progreso de la interpretacion del de pc
    [SerializeField]private GameObject _prefabIngrediente;
    private GameObject _contenedor;
    private const int aumento = 30;
    private int _alturaY = -92;
    private bool _actualizarHamburguesa = false;
    [SerializeField]
    private Texture[] Ingredientes;

    public GameObject topBunImgPrefab;

    //Añadir ingredientes a interpretacion
    public void AñadirPatyAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(1);
    }
    public void AñadirCheddarAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(2);
    }
    public void AñadirCebollaAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(3);
    }
    public void AñadirBaconAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(4);
        
    }
    public void AñadirLechugaAInterpretacion()
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        AñadirIngredienteAInterpretacion(5);
    }

    //limpiar interpretacion
    public void LimpiarInterpretacion()
    {
        _interpretacionDePC.Clear();
    }

    private void AñadirIngredienteAInterpretacion(int idIngrediente)
    {
        _interpretacionDePC.Add(idIngrediente);
        _actualizarHamburguesa = true;
    }

    public void SendInterpretacion()
    {

        if (_interpretacionDePC.Count <= 0) return;
        int[] interpretacion;

        GameObject a = Instantiate(topBunImgPrefab); //Creo el pan superior
        a.transform.SetParent(_contenedor.transform); //Lo pongo como hijo del contenedor que tiene a los otros panes
        a.transform.localPosition = new Vector3(9, 200, 0); //Falta arreglar la posición.
        a.transform.localScale = new Vector3(1, 1, 1);
        a.transform.localRotation = new Quaternion(0, 0, 0, 0);

        AñadirPanesAInterpretacion();
        interpretacion = AñadirPanesAInterpretacion();

        PedidoManager.instancePedidoManager.CrearInterpretacion(interpretacion);

        LimpiarInterpretacion();
    }

    private int[] AñadirPanesAInterpretacion()
    {
        int[] interpretacion = new int[_interpretacionDePC.Count + 2];
        interpretacion[0] = 0;
        for(int i = 1 ; i <= _interpretacionDePC.Count ; i++)
        {
            interpretacion[i] = _interpretacionDePC[i - 1];
        }
        interpretacion[interpretacion.Length - 1] = 0;
        return interpretacion;
    }

    private void Start()
    {
        _contenedor = GameObject.FindGameObjectWithTag("ContentMostrarHamburguesa");
    }

    private void Update()
    {
        if (!_actualizarHamburguesa) return;
        GameObject ingrediente = Instantiate(_prefabIngrediente);
        ingrediente.GetComponent<RawImage>().texture = Ingredientes[_interpretacionDePC[_interpretacionDePC.Count-1]];
        ingrediente.GetComponent<RectTransform>().position = new Vector3(9f,_alturaY , 0f);
        _alturaY = _alturaY + aumento;
        ingrediente.transform.SetParent(_contenedor.transform, false);
        _actualizarHamburguesa = false;
    }
}
