using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public void CrearInterpretacionHS()
    {
        PcManager.SetIdCombo(1);
    }
    public void CrearInterpretacionHD()
    {
        PcManager.SetIdCombo(2);
    }
    public void CrearInterpretacionHQ()
    {
        PcManager.SetIdCombo(3);
    }
    public void CrearInterpretacionHDQ()
    {
        PcManager.SetIdCombo(4);
    }
}
