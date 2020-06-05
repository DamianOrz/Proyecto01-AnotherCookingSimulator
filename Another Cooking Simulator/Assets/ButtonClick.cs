using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public void CrearInterpretacionHS()
    {
        PlayerMovement.SetIdCombo(1);
    }
    public void CrearInterpretacionHD()
    {
        PlayerMovement.SetIdCombo(2);
    }
    public void CrearInterpretacionHQ()
    {
        PlayerMovement.SetIdCombo(3);
    }
    public void CrearInterpretacionHDQ()
    {
        PlayerMovement.SetIdCombo(4);
    }
}
