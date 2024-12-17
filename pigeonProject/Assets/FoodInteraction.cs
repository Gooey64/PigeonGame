using UnityEngine;

public class FoodInteraction : MonoBehaviour
{
    public GameObject[] bubbleSpeechPanels; // Array of bubble speech panels
    public SoundMixerManager soundMixerManager;

    private int currentPanelIndex = 0; // Tracks the current panel
    private bool isPlayerNearby = false; // Check if player is near the food
    private bool gamePaused = false; // Pause state for the game
    private bool panelsSeen = false; // Tracks if the panels have already been seen

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
        // Check if the pigeon died or the player restarted
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            Debug.Log("Pigeon died or level restarted. Hiding all panels.");
            HideAllPanels();
            return;
        }

        // Allow the player to progress through the panels by pressing the right arrow key
        if (isPlayerNearby && gamePaused && 
            (Input.GetButtonDown("Action") || 
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
                panelsSeen = true; // Mark panels as seen
                ResumeGame(); // Resume the game when all panels are shown
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            Debug.Log("Pigeon died or level restarted. Panels will not be shown.");
            return;
        }

        if (other.CompareTag("Player") && !panelsSeen) // Only show panels if not already seen
        {
            Debug.Log("Player entered trigger zone. Showing bubble speech panels.");
            isPlayerNearby = true;

            // Activate the first panel when the player is near
            if (bubbleSpeechPanels.Length > 0 && currentPanelIndex < bubbleSpeechPanels.Length)
            {
                bubbleSpeechPanels[currentPanelIndex].SetActive(true);
            }

            PauseGame(); // Pause the game
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone. Hiding bubble speech panels.");
            isPlayerNearby = false;

            // Hide all panels when the player leaves
            HideAllPanels();
            ResumeGame(); // Resume the game
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
