using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour {

    public AudioMixer masterMixer;

    public TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;
    Canvas canvas;
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;

        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    
    public void Pause()
    {
        canvas.enabled = !canvas.enabled;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
    
    public void Quit()
    {
        #if UNITY_EDITOR 
        EditorApplication.isPlaying = false;
        #else 
        Application.Quit();
        #endif
    }
    

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

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void goBack()
    {
        canvas.enabled = !canvas.enabled;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}