using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiaInformacion", menuName = "ScriptableObjects/DiaInformacion", order = 1)]
public class DiaInformacion : ScriptableObject
{
    public int dia;
    public int maxIngredientesEntrePanes;

    public DiaManager.POSIBLES_INGREDIENTES[] posiblesIngredientes;
}
