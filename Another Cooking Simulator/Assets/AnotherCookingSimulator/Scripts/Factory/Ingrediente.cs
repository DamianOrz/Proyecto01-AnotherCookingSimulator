using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingrediente : MonoBehaviour
{
    [SerializeField] private string _id;

    public string Id => _id;
}
