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
            int[] idImages = new int[4] { 0, 1, 2, 3 };
            string[] dialogos = new string[4] { "Esto es una prueba", "Este es el segundo cuadro", "Holaaa", "holaaa2" };
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
        imageOfCanvas.sprite = listaImagenes[dialogo.GetIdImage()[_contDeCuadros]];
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
