using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic("MainMenuMusic");
                break;

            case "level1":
            case "level2":
            case "level3":
            case "level6":
            case "level7":
            case "level8":
            case "level11":
            case "level12":
            case "level13":
            case "level16":
            case "level17":
            case "level18":
                PlayMusic("Destroy");
                break;

            case "level4":
            case "level9":
            case "level14":
            case "level19":
                PlayMusic("Vip");
                break;

            case "level5":
            case "level10":
            case "level15":
                PlayMusic("Escape");
                break;

            case "level20":
                PlayMusic("Final");
                break;

            case "Launcher":
                PlaySfx("Lancement");
                break;
            default:
                break;
        }
    }


    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, s => s.name == name);//vérifie si la propriété name de chaque élément s dans le tableau est égale au paramètre name fourni.
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
        Sound sound = Array.Find(sfxSounds, s => s.name == name);//vérifie si la propriété name de chaque élément s dans le tableau est égale au paramètre name fourni.
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
