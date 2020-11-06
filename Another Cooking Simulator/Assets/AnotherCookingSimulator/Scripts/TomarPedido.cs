using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class TomarPedido : NetworkBehaviour
{
    private List<int> _interpretacionDePC = new List<int>();
    private int _numeroDeMesa = 0;

    //Mostrar el progreso de la interpretacion del de pc
    [SerializeField]private GameObject _prefabIngrediente;
    private GameObject _contenedor;
    [SerializeField] private int _maxIngredientes = 5;
    private const int _aumento = 30;
    [SyncVar]
    private int _alturaY = -92;
    private bool _actualizarHamburguesa = false;

    #region eleccionDeMesa
    [SerializeField] private GameObject leftPart;
    [SerializeField] private GameObject rightPart;
    [SerializeField] private GameObject seleccionDeMesa;
    #endregion
    [SerializeField]private Texture[] _ingredientes;

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
    public void AñadirMesaAInterpretacion(int numeroDeLaMesa)
    {
        FindObjectOfType<AudioManager>().Play("FX-Tap");
        _numeroDeMesa = numeroDeLaMesa;
    }
    //limpiar interpretacion
    public void LimpiarInterpretacion()
    {
        _interpretacionDePC.Clear();
        _numeroDeMesa = 0;
        _actualizarHamburguesa = true;
    }

    private void AñadirIngredienteAInterpretacion(int idIngrediente)
    {
        if (_interpretacionDePC.Count > _maxIngredientes) return;
        _interpretacionDePC.Add(idIngrediente);
        _actualizarHamburguesa = true;
    }
    public void ContinueInterpretacion()
    {
        if (_interpretacionDePC.Count <= 0) return;
        leftPart.SetActive(false); rightPart.SetActive(false);seleccionDeMesa.SetActive(true);
        
    }
    public void SendInterpretacion()
    {

        if (_numeroDeMesa == 0) return;
        Debug.Log("SIMON: LA MESA DE LA INTERPRETACION ES : " + _numeroDeMesa);
        int[] interpretacion;

        GameObject panSuperior = Instantiate(_prefabIngrediente); //Creo el pan superior

        panSuperior.GetComponent<RawImage>().texture = _ingredientes[0];
        panSuperior.transform.SetParent(_contenedor.transform, false);

        AñadirPanesAInterpretacion();
        interpretacion = AñadirPanesAInterpretacion();

        PedidoManager.instancePedidoManager.CrearInterpretacion(interpretacion);

        PedidoManager.BorrarPedidoDelCanvas(_numeroDeMesa);

        LimpiarInterpretacion();

        
        leftPart.SetActive(true); rightPart.SetActive(true); seleccionDeMesa.SetActive(false);
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
        //GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToServer);
        _contenedor = GameObject.FindGameObjectWithTag("ContentMostrarHamburguesa");
    }

    private void Update()
    {
        if (!_actualizarHamburguesa) return;
        if (_interpretacionDePC.Count == 0)
        {
            LimpiarIngredientes();
        }else
        {
            AñadirIngredienteServer(_interpretacionDePC[_interpretacionDePC.Count - 1]);
            //GameObject ingrediente = Instantiate(_prefabIngrediente);
            //ingrediente.GetComponent<RawImage>().texture = _ingredientes[_interpretacionDePC[_interpretacionDePC.Count-1]];
            //ingrediente.GetComponent<RectTransform>().position = new Vector3(9f,_alturaY , 0f);
            //_alturaY = _alturaY + _aumento;
            //ingrediente.transform.SetParent(_contenedor.transform, false);
        }
        _actualizarHamburguesa = false;
    }
    [Server]
    public void AñadirIngredienteServer(int indexIngrediente)
    {
        RpcAñadirIngredienteAlCanvas(indexIngrediente);
    }

    [ClientRpc]
    private void RpcAñadirIngredienteAlCanvas(int indexIngrediente)
    {
        GameObject ingrediente = Instantiate(_prefabIngrediente);
        ingrediente.GetComponent<RawImage>().texture = _ingredientes[indexIngrediente];
        ingrediente.GetComponent<RectTransform>().position = new Vector3(9f, _alturaY, 0f);
        _alturaY = _alturaY + _aumento;
        ingrediente.transform.SetParent(_contenedor.transform, false);
    }
    private void LimpiarIngredientes()
    {
        foreach (Transform child in _contenedor.transform)
        {
            if (!(child.gameObject.name == "PanInferior" || child.gameObject.name == "PanSuperior"))
            {
                Destroy(child.gameObject);
            }
        }
        _alturaY = -92;
    }
}
