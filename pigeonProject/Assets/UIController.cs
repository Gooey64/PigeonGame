using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider;

    private void Start()
    {
        _musicSlider.value = (_musicSlider.minValue + _musicSlider.maxValue) / 2;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void MusicVolume()
    {
        float volume = _musicSlider.value;
        AudioManager.Instance.MusicVolume(volume);
        AudioManager.Instance.SFXVolume(volume);

        EventSystem.current.SetSelectedGameObject(null);
    }
}

