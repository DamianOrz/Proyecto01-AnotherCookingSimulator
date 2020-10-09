using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager instanceDialogoInformacion;

    private Canvas canvasDialogo;
    [SerializeField] private Sprite[] listaImagenes;
    private TMP_Text mensaje;
    private Image imageOfCanvas;
    private Dialogo elDialogo = new Dialogo();
    private int _contDeCuadros = -1;

    [SerializeField]
    //private string _questionPath;
    //private DialogoCollection _dialogosCollection;

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
        //CargarDialogos();
        canvasDialogo = GetComponent<Canvas>();
        GameObject panel = canvasDialogo.transform.Find("Panel").gameObject;
        mensaje = panel.transform.GetComponentInChildren<TMP_Text>();
        imageOfCanvas = panel.transform.GetChild(0).transform.GetComponent<Image>();
        canvasDialogo.enabled = false;
        int[] idImages = new int[7] { 0, 1, 2, 3, 0, 1, 2 };
        string[] dialogos = new string[7] { "Bienvenido al tutorial! Soy el dueño del Restaurante ACS.",
                "Aca aprenderas a hacer las tareas en tu nuevo trabajo aqui.",
                "Lo primero que tenes que aprender es a moverte por el espacio asi no te mandas ninguna cagada, mira el poster de atras tuyo para saber como hacerlo.",
                "Ahora que ya te sabes mover, quiero que me des una hamburguesa simple",
                "Ve a buscar la hamburguesa en la mesa de en frente",
                "Ahora llevala a la parte violeta del mostrador anterior para que me la pueda comer pa",
                "Esta un poco cruda pero para ser la primera vez zafa, ahora ya puedes salir por la puerta y a la izquierda veras el restaurante" };
        //hacerDialogo(idImages, dialogos.Length, dialogos);
    }

    //private void CargarDialogos()
    //{
    //    using (StreamReader stream = new StreamReader(_questionPath))
    //    {
    //        string json = stream.ReadToEnd();
    //        _dialogosCollection = JsonUtility.FromJson<DialogoCollection>(json);
    //    }
    //}

    private void Update()
    {
        if (DiaManager.instanceDiaManager.isCanvasBeingUsed()) return;
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
