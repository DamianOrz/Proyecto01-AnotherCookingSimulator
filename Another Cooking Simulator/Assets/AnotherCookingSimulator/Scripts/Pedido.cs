using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Pedido
{
    public int _intId=-1;
    public string _ordenIngredientes;

    public Pedido(int intId, string ordenIngredientes)
    {
        this._intId = intId;
        this._ordenIngredientes = ordenIngredientes;
    }
    public Pedido(string ordenIngredientes)
    {
        this._intId++;
        this._ordenIngredientes = ordenIngredientes;
    }
    public Pedido()
    {

    }

    public int getId()
    {
        return this._intId;
    }

    public void setID(int value)
    {
        this._intId = value;
    }
    public string getOrdenIngredientes()
    {
        return this._ordenIngredientes;
    }

    public void setOrdenIngredientes(string value)
    {
        this._ordenIngredientes = value;
    }
}