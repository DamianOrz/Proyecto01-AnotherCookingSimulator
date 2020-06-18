using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaManager : MonoBehaviour
{
    public static int diaActual=0;
    public enum POSIBLES_INGREDIENTES
    {
        PAN,
        CARNE,
        QUESO,
        CEBOLLA,
        BACON,
        LECHUGA,
        TOMATE
    }
    public static DiaInformacion[] diasInfoStc;
    public  DiaInformacion[] diasInfo;
    void Start()
    {
        diasInfoStc = diasInfo;
    }
}
