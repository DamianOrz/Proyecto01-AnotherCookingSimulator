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
    private int[] _OrdenIngredientes;
    private int[] _InterpretacionIngredientes;
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

    public int[] GetOrdenIngredientes()
    {
        return _OrdenIngredientes;
    }
    public void SetOrdenIngredientes(int[] OrdenIngredientes)
    {
        _OrdenIngredientes = OrdenIngredientes;
    }

    public int[] GetInterpretacionIngredientes()
    {
        return _InterpretacionIngredientes;
    }
    public void SetInterpretacionIngredientes(int[] InterpretacionIngredientes)
    {
        _InterpretacionIngredientes = InterpretacionIngredientes;
    }
    #endregion
    #region Constructors
    public Pedido(int intId, int[] OrdenIngredientes)
    {
        this._intIdPedido = intId;
        this._OrdenIngredientes = OrdenIngredientes;
    }
    public Pedido(int[] OrdenIngredientes)
    {
        this._OrdenIngredientes = OrdenIngredientes;
    }
    public Pedido()
    {

    }
    #endregion
}