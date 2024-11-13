using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private SoundMixerManager soundMixerManager;

    private void Start()
    {
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        masterVolumeSlider.value = savedMasterVolume;
        musicVolumeSlider.value = savedMusicVolume;
        sfxVolumeSlider.value = savedSFXVolume;

        ApplyVolumeLevels(savedMasterVolume, savedMusicVolume, savedSFXVolume);

        masterVolumeSlider.onValueChanged.AddListener(value => AdjustMasterVolume(value));
        musicVolumeSlider.onValueChanged.AddListener(value => AdjustMusicVolume(value));
        sfxVolumeSlider.onValueChanged.AddListener(value => AdjustSFXVolume(value));
    }

    public void PlayGame() 
    {
        // Save volume settings before loading the game
        // PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        // PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        // PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);

        // SceneManager.LoadSceneAsync("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void AdjustMasterVolume(float level)
    {
        PlayerPrefs.SetFloat("MasterVolume", level); 
        soundMixerManager.SetMasterVolume(level);     
    }

    private void AdjustMusicVolume(float level)
    {
        PlayerPrefs.SetFloat("MusicVolume", level); 
        soundMixerManager.SetMusicVolume(level);  
    }

    private void AdjustSFXVolume(float level)
    {
        PlayerPrefs.SetFloat("SFXVolume", level); 
        soundMixerManager.SetSoundFXVolume(level); 
    }

    private void ApplyVolumeLevels(float masterVolume, float musicVolume, float sfxVolume)
    {
        soundMixerManager.SetMasterVolume(masterVolume);
        soundMixerManager.SetMusicVolume(musicVolume);
        soundMixerManager.SetSoundFXVolume(sfxVolume);
    }
}
