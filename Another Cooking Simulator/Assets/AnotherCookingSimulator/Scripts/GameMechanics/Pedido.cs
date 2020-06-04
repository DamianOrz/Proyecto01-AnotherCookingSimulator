using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Pedido
{
    private int _intIdPedido = 1;
    private int _intNumMesa;
    private List<String> _OrdenIngredientes;
    private List<String> _InterpretacionIngredientes;

    public int IntNumMesa { get => _intNumMesa; set => _intNumMesa = value; }
    public List<string> OrdenIngredientes { get => _OrdenIngredientes; set => _OrdenIngredientes = value; }
    public List<string> InterpretacionIngredientes { get => _InterpretacionIngredientes; set => _InterpretacionIngredientes = value; }
    public int IntIdPedido { get => _intIdPedido; set => _intIdPedido = value; }

    public Pedido(int intId, List<String> strOrdenIngredientes)
    {
        this._intIdPedido = intId;
        this._strOrdenIngredientes = strOrdenIngredientes;
    }
    public Pedido(List<String> ordenIngredientes)
    {
        this._intIdPedido++;
        this.OrdenIngredientes = ordenIngredientes;
    }
    public Pedido()
    {

    }
}