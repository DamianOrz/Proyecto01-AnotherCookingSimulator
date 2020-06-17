using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosiblesIngredientesManager : MonoBehaviour
{
    public enum POSIBLES_INGREDIENTES
    {
        CARNE,
        QUESO,
        CEBOLLA,
        BACON,
        LECHUGA,
        TOMATE
    }
    public PosiblesIngredientesManager[] recipes;
}
