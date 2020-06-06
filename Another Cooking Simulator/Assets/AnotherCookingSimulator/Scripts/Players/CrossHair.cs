using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public RectTransform crossHair;
    private void Start()
    {
        crossHair = GetComponent<RectTransform>();
    }
}
