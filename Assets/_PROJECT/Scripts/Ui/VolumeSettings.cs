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
   

    private void Awake()
    {
        _musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        _SFXSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();

        
    }
   
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

    public void MuteMusicToggle()
    {
        if (_musicSlider.value > 0.0001)
        {
            _musicSlider.value = 0f;
        }
        else
        {
            _musicSlider.value = 1;
        }
        SetMusicVolume() ;
    }

    public void MuteSFXToggle()
    {
        if (_SFXSlider.value > 0.0001)
        {
            _SFXSlider.value = 0f;
        }
        else
        {
            _SFXSlider.value = 1;
        }
        SetSFXVolume();
    }
}
