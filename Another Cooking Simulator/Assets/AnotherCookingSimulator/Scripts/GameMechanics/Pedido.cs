using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Pedido
{
    #region Variables Privadas
    private int _intIdPedido = 0;
    private int _intNumMesa;
    private String  _OrdenIngredientes;
    private List<String> _InterpretacionIngredientes;
    #endregion
    #region Getter and Setters 
    public int GetIdPedido()
    {
        return _intIdPedido;
    }
    public void SetIdPedido(int intIdPedido)
    {
        _intIdPedido = intIdPedido;
    }

    public int GetNumMesa()
    {
        return _intNumMesa;
    }
    public void SetNumMesa(int numMesa)
    {
        _intNumMesa = numMesa;
    }

    public String GetOrdenIngredientes()
    {
        return _OrdenIngredientes;
    }
    public void SetOrdenIngredientes(String OrdenIngredientes)
    {
        _OrdenIngredientes = OrdenIngredientes;
    }

    public List<String> GetInterpretacionIngredientes()
    {
        return _InterpretacionIngredientes;
    }
    public void SetInterpretacionIngredientes(List<String> InterpretacionIngredientes)
    {
        _InterpretacionIngredientes = InterpretacionIngredientes;
    }
    #endregion
    #region Constructors
    public Pedido(int intId, String OrdenIngredientes)
    {
        this._intIdPedido = intId;
        this._OrdenIngredientes = OrdenIngredientes;
    }
    public Pedido(String OrdenIngredientes)
    {
        this._OrdenIngredientes = OrdenIngredientes;
    }
    public Pedido()
    {
        this._intIdPedido++;
    }
    #endregion
}