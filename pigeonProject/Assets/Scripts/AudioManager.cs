using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public SoundControl[] musicSounds, sfxSounds;
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

    // private void Start()
    // {
    //     // PlayMusic("Theme"); 
    // }

    public void PlayLevelMusic(string levelName)
    {
       
        SoundControl s = Array.Find(musicSounds, x => x.name == levelName);
        if (s == null)
        {
            Debug.LogWarning("Music Sound Not Found for level: " + levelName);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayButtonClick()
    {
        PlaySFX("Button Noise");
    }

    public void PlayMusic(string name) 
    {
        // SoundControl s = Array.Find(musicSounds, x => x.name == name);
        // if (s == null)
        // {
        //     Debug.LogWarning("Music Sound Not Found: " + name);
        // }
        // else 
        // {
        //     musicSource.clip = s.clip;
        //     musicSource.Play();
        // }
    }

    public void PlaySFX(string name) 
    {
        SoundControl s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null) 
        {
            Debug.LogWarning("SFX Sound Not Found: " + name);
        }
        else 
        {
            sfxSource.PlayOneShot(s.clip); 
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}


