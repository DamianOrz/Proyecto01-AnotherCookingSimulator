using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectorManager : MonoBehaviour
{
    public GameObject btnDay0; //Tutorial
    public GameObject btnDay1;
    public GameObject btnDay2;
    public GameObject btnDay3;
    public GameObject btnDay4;
    public GameObject btnDay5;

    // Start is called before the first frame update
    void Start()
    {
        btnDay0.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateAllButtons(bool status)
    {
        btnDay0.GetComponent<Button>().interactable = status;
        btnDay1.GetComponent<Button>().interactable = status;
        btnDay2.GetComponent<Button>().interactable = status;
        btnDay3.GetComponent<Button>().interactable = status;
        btnDay4.GetComponent<Button>().interactable = status;
        btnDay5.GetComponent<Button>().interactable = status;
    }



    public void LevelChanged(Button btnSelected)
    {
        UpdateAllButtons(true);

        btnSelected.interactable = false;

    }




}
