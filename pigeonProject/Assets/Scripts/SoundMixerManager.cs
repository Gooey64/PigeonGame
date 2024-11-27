using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
   [SerializeField] private AudioMixer audioMixer;
    private Slider volumeCtrl;
    public static float volumeLvl = 1.0f;

    private void Awake()
    {
        SetMasterVolume(volumeLvl);
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        volumeLvl = level;
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        volumeLvl = level;
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        volumeLvl = level;
    }
}
