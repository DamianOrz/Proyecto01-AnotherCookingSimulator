﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
