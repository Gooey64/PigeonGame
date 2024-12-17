using UnityEngine;
using System.Collections;

public class PigeonMovementWithPanel : MonoBehaviour
{
    public GameObject[] initialPanels;

    public SoundMixerManager soundMixerManager;
    private int currentPanelIndex = 0;
    private bool gamePaused = true;
    private static bool panelsShownOnce = false; 

    void Start()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            Debug.Log("Player has died or level restarted. Disabling all panels.");
            DisableAllPanels();
            return;
        }

        if (panelsShownOnce)
        {
            Debug.Log("Panels have already been shown. Skipping initial panels.");
            ResumeGame();
            return;
        }

        if (initialPanels.Length > 0)
        {
            Time.timeScale = 0;
            gamePaused = true;
            Debug.Log("Game paused. Displaying initial panels.");

            for (int i = 0; i < initialPanels.Length; i++)
            {
                initialPanels[i].SetActive(i == 0); 
            }

            panelsShownOnce = true; 
        }
        else
        {
            Debug.LogWarning("No initial panels found. Skipping initial panel setup.");
            ResumeGame();
        }
    }

    void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            DisableAllPanels();
            return;
        }

        if (gamePaused && (Input.GetKeyDown(KeyCode.RightArrow) ||
                           Input.GetButtonDown("Action") ||
                           Input.GetKeyDown(KeyCode.D)))
        {
            HideCurrentPanel();
        }
    }

    void HideCurrentPanel()
    {
        if (currentPanelIndex < initialPanels.Length)
        {
            initialPanels[currentPanelIndex].SetActive(false);
            currentPanelIndex++;

            if (currentPanelIndex < initialPanels.Length)
            {
                initialPanels[currentPanelIndex].SetActive(true);
            }
            else
            {
                ResumeGame();
            }
        }
        else
        {
            Debug.LogWarning("No more initial panels to display.");
        }
    }

    void PauseGame()
    {
        if (initialPanels.Length == 0)
        {
            Debug.LogWarning("No panels available. Game will not pause.");
            return;
        }

        gamePaused = true;
        Time.timeScale = 0;
        PauseSFX();
        Debug.Log("Game paused.");
    }

    void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        ResumeSFX();
        Debug.Log("Game resumed.");
    }

    void PauseSFX()
    {
        if (soundMixerManager != null)
        {
            soundMixerManager.SetSoundFXVolume(0.0001f);
        }
    }

    void ResumeSFX()
    {
        if (soundMixerManager != null)
        {
            soundMixerManager.SetSoundFXVolume(1f);
        }
    }

    void DisableAllPanels()
    {
        foreach (var panel in initialPanels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        Debug.Log("All panels have been disabled.");
    }
}
