using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("MainMenuMusic");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, s => s.name == name);//v�rifie si la propri�t� name de chaque �l�ment s dans le tableau est �gale au param�tre name fourni.
        if (sound == null)
        {
            Debug.Log("Music not found !");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound sound = Array.Find(sfxSounds, s => s.name == name);//v�rifie si la propri�t� name de chaque �l�ment s dans le tableau est �gale au param�tre name fourni.
        if (sound == null)
        {
            Debug.Log("Sfx not found !");
        }
        else
        {
            
            sfxSource.PlayOneShot(sound.clip);
        }
    }
}
