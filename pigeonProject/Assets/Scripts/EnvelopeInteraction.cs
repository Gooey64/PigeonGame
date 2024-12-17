using UnityEngine;

public class EnvelopeInteraction : MonoBehaviour
{
    public GameObject[] bubbleSpeechPanels; // Array of bubble speech panels
    public SoundMixerManager soundMixerManager;

    private int currentPanelIndex = 0; // Tracks the current panel
    private bool isPlayerNearby = false;
    private bool isPickedUp = false;
    private bool gamePaused = false;
    private bool hasPanelDisplayed = false;

    private void Start()
    {
        // Ensure all panels are initially inactive
        foreach (var panel in bubbleSpeechPanels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            HideAllPanels();
            return;
        }

        if (isPlayerNearby && gamePaused && (Input.GetButtonDown("Action") || 
             Input.GetKeyDown(KeyCode.RightArrow) || 
             Input.GetKeyDown(KeyCode.D)))
        {
            ShowNextPanel();
        }
    }

    private void ShowNextPanel()
    {
        if (currentPanelIndex < bubbleSpeechPanels.Length)
        {
            bubbleSpeechPanels[currentPanelIndex].SetActive(false); // Hide the current panel
            currentPanelIndex++;

            if (currentPanelIndex < bubbleSpeechPanels.Length)
            {
                bubbleSpeechPanels[currentPanelIndex].SetActive(true); // Show the next panel
            }
            else
            {
                ResumeGame(); // Resume the game when all panels are shown
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            HideAllPanels();
            return;
        }

        if (other.CompareTag("Player") && !isPickedUp && !hasPanelDisplayed)
        {
            Debug.Log("Player entered trigger zone: Showing bubble speech panels.");
            isPlayerNearby = true;

            // Activate the first panel when the player is near
            if (bubbleSpeechPanels.Length > 0 && currentPanelIndex < bubbleSpeechPanels.Length)
            {
                bubbleSpeechPanels[currentPanelIndex].SetActive(true);
            }

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

            HideAllPanels();
            ResumeGame();
        }
    }

    private void HideAllPanels()
    {
        foreach (var panel in bubbleSpeechPanels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        currentPanelIndex = 0; // Reset panel index
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
