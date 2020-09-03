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
    private bool isCanvasEnabled = false;

    Resolution[] resolutions;
    static Canvas canvas;
    
    void Start()
    {
        QualitySettings.SetQualityLevel(2);

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
        if (DiaManager.instanceDiaManager.isCanvasBeingUsed()) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pause(); --> No queremos pausar el juego en este caso
            UpdateCanvasStatus();           
        }
    }


    private void UpdateCanvasStatus()
    {
        canvas.enabled = !canvas.enabled;

        Cursor.lockState = CursorLockMode.None;

        if (canvas.enabled)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public static bool isCanvasBeingUsed()
    {
        if (canvas.enabled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void Pause()
    {
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
        UpdateCanvasStatus();
    }
}