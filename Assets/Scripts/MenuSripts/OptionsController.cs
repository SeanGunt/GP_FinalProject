using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//work in progress

public class OptionsController : MonoBehaviour
{
    public AudioMixer mixer;

    [SerializeField]
    Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("GameVolume"))
        {
            PlayerPrefs.SetFloat("GameVolume", 1);
        }
        //
        //
        Load();
            
    }

    public void VolumeSlider(float sliderValue)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
    }

    void Save()
    {
        //PlayerPrefs
        //PlayerPrefs.
        PlayerPrefs.SetFloat("GameVolume", volumeSlider.value);
    }

    void Load()
    {
        //
        //
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume");
    }
}
