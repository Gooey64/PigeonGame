using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInteraction : MonoBehaviour
{
    public GameObject bubbleSpeech;
    public SoundMixerManager soundMixerManager;

    private bool isPlayerNearby = false;
    private bool gamePaused = false;
    private bool hasPanelDisplayed = false;

    private void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            bubbleSpeech.SetActive(false);
            return;
        }

        if (isPlayerNearby && gamePaused && (Input.GetKeyDown(KeyCode.F) || 
                                             Input.GetKeyDown(KeyCode.RightArrow) || 
                                             Input.GetKeyDown(KeyCode.D)))
        {
            ResumeGame();
            bubbleSpeech.SetActive(false); 
            Debug.Log("Bubble speech deactivated.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            bubbleSpeech.SetActive(false);
            return;
        }

        if (other.CompareTag("Player") && !hasPanelDisplayed)
        {
            Debug.Log("Player entered trigger zone: Showing bubble speech and freezing game.");
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
            Debug.Log("Player exited trigger zone.");
            isPlayerNearby = false;
            bubbleSpeech.SetActive(false);
        }
    }

    private void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        MuteSFX();
        Debug.Log("Game paused.");
    }

    private void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        RestoreSFX();
        Debug.Log("Game resumed.");
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
