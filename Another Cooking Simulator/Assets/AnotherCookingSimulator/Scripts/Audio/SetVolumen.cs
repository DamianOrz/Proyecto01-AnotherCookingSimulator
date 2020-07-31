using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolumen : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetMasterVolume(float sliderMasterVolume)
    {
        masterMixer.SetFloat("masterVolume", sliderMasterVolume);
    }

    public void SetMusicVolume(float sliderMusicVolume)
    {
        masterMixer.SetFloat("musicVolume", sliderMusicVolume);
    }

    public void SetFXVolume(float sliderFXVolume)
    {
        masterMixer.SetFloat("FXVolume", sliderFXVolume);
    }
}
