using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaManager : MonoBehaviour
{
    public TMP_Text texto;
    public static TMP_Text textoStc;

    public static int diaActual = -1;
    private int clientes;

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
    public DiaInformacion[] diasInfo;
    void Start()
    {
        diasInfoStc = diasInfo;
        textoStc = texto;
    }

    public static void EmpezarDia()
    {
        diaActual++;
        textoStc.text = "Dia : " + (diaActual);
    }
    public static void FinalizarDia()
    {
        int score = ScoreManager.getScore();
        EmpezarDia();
    }
    public static void LlevarPersonajesAlMenu()
    {

    }
}
