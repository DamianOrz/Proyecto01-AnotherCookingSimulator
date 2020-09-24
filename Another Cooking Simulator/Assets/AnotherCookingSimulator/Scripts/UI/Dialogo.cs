using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    private int[] _idImage;
    private int _cantidadDeCuadros;
    private string[] _msg;

    public Dialogo(int[] idImage, int cantidadDeCuadros, string[] msg)
    {
        _idImage = idImage;
        _cantidadDeCuadros = cantidadDeCuadros;
        _msg = msg;
    }
    public Dialogo()
    {

    }

    public int[] GetIdImage()
    {
        return _idImage;
    }
    public int GetCantidadDeCuadros()
    {
        return _cantidadDeCuadros;
    }
    public string[] GetMsg()
    {
        return _msg;
    }

    public void SetIdImage(int[] id)
    {
        _idImage = id;
    }
    public void SetCantidadDeCuadros(int cant)
    {
        _cantidadDeCuadros = cant;
    }
    public void SetDialogo(string[] dialogoParaCadaCuadro)
    {
        _msg = dialogoParaCadaCuadro;
    }
}
