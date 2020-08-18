using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text tmpTexto;
    public static TMP_Text texto;
    private static int SCORE = 0;

    // Start is called before the first frame update
    void Start()
    {
        texto = tmpTexto;
        sobreEscribir(0);
    }
    public static int getScore()
    {
        return SCORE;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public static void sobreEscribir(int puntos)
    {
        SCORE += puntos;
        texto.text = "Score: " + SCORE;
    }
}
