using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public Slider masterVolumeSlider;
    public Slider soundFXVolumeSlider;
    public Slider musicVolumeSlider;

    private void Start()
    {
        LoadVolumeSettings();

        UpdateSliders();
    }

    public void SetMasterVolume(float level) 
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", level); 
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("SoundFXVolume", level); 
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", level); 
    }

    private void LoadVolumeSettings()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f); 
        float soundFXVolume = PlayerPrefs.GetFloat("SoundFXVolume", 1f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        audioMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20f);
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(soundFXVolume) * 20f);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20f);
    }

    private void UpdateSliders()
    {
        if (masterVolumeSlider != null)
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);

        if (soundFXVolumeSlider != null)
            soundFXVolumeSlider.value = PlayerPrefs.GetFloat("SoundFXVolume", 1f);

        if (musicVolumeSlider != null)
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
}
