using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    Dropdown graphicsDropdown;

    [SerializeField]
    Toggle cheatToggle;

    void Start()
    {
        //sets default playerprefs if they don't exist
        if (!PlayerPrefs.HasKey("GameVolume"))
        {
            PlayerPrefs.SetFloat("GameVolume", 1);
        }
        if (!PlayerPrefs.HasKey("Graphics"))
        {
            PlayerPrefs.SetInt("Graphics", 3);
        }
        if (!PlayerPrefs.HasKey("TimeSlowCheat"))
        {
            PlayerPrefs.SetInt("TimeSlowCheat", 0);
        }
        LoadValues();   
    }

    public void SetVolume(float sliderValue)
    {
        //sets volume (logarithmic) and assigns volume playerpref
        mixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("GameVolume", sliderValue);

        Debug.Log("Volume: " + PlayerPrefs.GetFloat("GameVolume"));
    }

    public void SetGraphics(int qualityIndex)
    {
        //sets graphics and assigns graphics playerpref
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Graphics", qualityIndex);

        Debug.Log("Graphics: " + PlayerPrefs.GetInt("Graphics"));
    }

    public void SetTimeSlowCheat(bool cheatBool)
    {
        //assigns cheat playerpref (to be used as conditional in time slow mechanic)
        PlayerPrefs.SetInt("TimeSlowCheat", cheatBool ? 1:0); //converts boolean to int
    }

    void LoadValues()
    {
        //loads object values
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume");
        graphicsDropdown.value = PlayerPrefs.GetInt("Graphics");
        cheatToggle.isOn = PlayerPrefs.GetInt("TimeSlowCheat") == 1 ? true:false; //converts int to boolean

        //sets each setting
        SetVolume(PlayerPrefs.GetFloat("GameVolume"));
        SetGraphics(PlayerPrefs.GetInt("Graphics"));
        SetTimeSlowCheat(PlayerPrefs.GetInt("TimeSlowCheat") == 1 ? true:false);
    }
}
