using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public GameObject bubbleSpeech;
    public SoundMixerManager soundMixerManager;

    private bool isPlayerNearby = false;
    private bool isPickedUp = false;
    private bool gamePaused = false;
    private bool hasPanelDisplayed = false;

    private void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            bubbleSpeech.SetActive(false);
            return;
        }

        if (gamePaused && (Input.GetKeyDown(KeyCode.RightArrow) || 
                           Input.GetButtonDown("Action") || 
                           Input.GetKeyDown(KeyCode.D)))
        {
            ResumeGame();
            bubbleSpeech.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            bubbleSpeech.SetActive(false);
            return;
        }

        if (other.CompareTag("Player") && !isPickedUp && !hasPanelDisplayed)
        {
            isPlayerNearby = true;
            bubbleSpeech.SetActive(true);
            PauseGame();
            hasPanelDisplayed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            bubbleSpeech.SetActive(false);
        }
    }

    private void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        MuteSFX();
    }

    private void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        RestoreSFX();
    }

    private void MuteSFX()
    {
        if (soundMixerManager != null)
        {
            soundMixerManager.SetSoundFXVolume(0.0001f);
        }
    }

    private void RestoreSFX()
    {
        if (soundMixerManager != null)
        {
            soundMixerManager.SetSoundFXVolume(1f);
        }
    }
}
