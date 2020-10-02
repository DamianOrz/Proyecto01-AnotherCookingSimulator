using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager instanceDialogoInformacion;

    private Canvas canvasDialogo;
    [SerializeField] private Sprite[] listaImagenes;
    private TMP_Text mensaje;
    private Image imageOfCanvas;
    private Dialogo elDialogo = new Dialogo();
    private int _contDeCuadros = -1;

    private void Awake()
    {
        if (instanceDialogoInformacion != null && instanceDialogoInformacion != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanceDialogoInformacion = this;
        }
    }
    private void Start()
    {
        canvasDialogo = GetComponent<Canvas>();
        GameObject panel = canvasDialogo.transform.Find("Panel").gameObject;
        mensaje = panel.transform.GetComponentInChildren<TMP_Text>();
        imageOfCanvas = panel.transform.GetChild(0).transform.GetComponent<Image>();
        canvasDialogo.enabled = false;
    }

    private void Update()
    {
        if (DiaManager.instanceDiaManager.isCanvasBeingUsed()) return;
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            int[] idImages = new int[3] { 0, 1, 2};
            string[] dialogos = new string[3] { "Bienvenido al tutorial! Soy el dueño del Restaurante ACS.", "Aca aprenderas a hacer las tareas en tu nuevo trabajo aqui.", "Lo primero que tenes que aprender es a moverte por el espacio asi no te mandas ninguna cagada, mira el poster de en frente tuyo para saber como hacerlo."};
            hacerDialogo(idImages, dialogos.Length, dialogos);
        }
        if (canvasDialogo.enabled)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (!(_contDeCuadros == elDialogo.GetMsg().Length))
                {
                    mostrarCuadro(elDialogo);
                }
                else
                {
                    Continue();
                }
                UpdateCursor();
            }
        }
    }
    private void Continue()
    {
        canvasDialogo.enabled = false;
    }

    public void hacerDialogo(int[] idImg, int cantidadDeCuadros, string[] msg)
    {
        _contDeCuadros = 0;
        elDialogo.SetIdImage(idImg);
        elDialogo.SetCantidadDeCuadros(cantidadDeCuadros);
        elDialogo.SetDialogo(msg);

        mostrarCuadro(elDialogo);

        canvasDialogo.enabled = true;
    }
    private void mostrarCuadro(Dialogo dialogo)
    {
        GameObject panel = canvasDialogo.transform.Find("Panel").gameObject;
        Animation introAnimation = panel.GetComponent<Animation>();
        if (_contDeCuadros == 0) introAnimation.Play();

        //imageOfCanvas.sprite = listaImagenes[dialogo.GetIdImage()[_contDeCuadros]]; --> Mejor no
        mensaje.text = dialogo.GetMsg()[_contDeCuadros];

        UpdateCursor();
        _contDeCuadros++;
    }
    private void UpdateCursor()
    {
        Cursor.lockState = CursorLockMode.None;

        if (canvasDialogo.enabled)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
