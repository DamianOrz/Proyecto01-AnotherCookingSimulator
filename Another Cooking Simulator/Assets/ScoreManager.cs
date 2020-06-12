using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static int SCORE = 0;
    public static TMP_Text texto;

    // Start is called before the first frame update
    void Start()
    {
        sobreEscribir(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void sobreEscribir(int puntos)
    {
        SCORE += puntos;
        //texto.text = "Score: " + SCORE.ToString();
    }
}
