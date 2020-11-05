using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMenu : MonoBehaviour
{
    private bool bMenuShowing;
    public GameObject LevelSelectorPanel;
    private Animation levelSelectorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        bMenuShowing = false;
        levelSelectorAnimator = LevelSelectorPanel.GetComponent<Animation>();
    }

    // Update is called once per frame
    public void ShowMenu()
    {
        if (bMenuShowing) //Si está activado lo guardamos
        {
            levelSelectorAnimator.Play("HideLevelSelector");
        }
        else //Lo mostramos
        {
            levelSelectorAnimator.Play("ShowLevelSelector");
        }
        bMenuShowing = !bMenuShowing;
    }
}
