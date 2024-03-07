using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private AudioMixer _audioMixer;
    public bool _musicOn;
    public bool _SFXOff;


    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

    }
    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("Music", Mathf.Log10(_musicSlider.value) * 20);

        //save parameters
        PlayerPrefs.SetFloat("musicVolume", volume);

    }

    public void SetSFXVolume()
    {
        float volume = _SFXSlider.value;
        //convertit la valeur linéaire du slider en une valeur logarithmique en dB.
        //Chaque incrément de 20 dB correspond à un facteur de 10 dans l’intensité.
        _audioMixer.SetFloat("SFX", Mathf.Log10(_musicSlider.value) * 20);

        //save parameters
        PlayerPrefs.SetFloat("SFX", volume);

    }
    private void LoadVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _SFXSlider.value = PlayerPrefs.GetFloat("SFX");
        SetMusicVolume();
    }
}
