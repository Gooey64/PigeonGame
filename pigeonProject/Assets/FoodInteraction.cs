using UnityEngine;

public class FoodInteraction : MonoBehaviour
{
    public GameObject[] bubbleSpeechPanels;
    public SoundMixerManager soundMixerManager;

    private int currentPanelIndex = 0;
    private bool isPlayerNearby = false; 
    private bool gamePaused = false; 
    private bool panelsSeen = false;

    private void Start()
    {
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
            Debug.Log("Pigeon died or level restarted. Hiding all panels.");
            HideAllPanels();
            return;
        }

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
            bubbleSpeechPanels[currentPanelIndex].SetActive(false); 
            currentPanelIndex++;

            if (currentPanelIndex < bubbleSpeechPanels.Length)
            {
                bubbleSpeechPanels[currentPanelIndex].SetActive(true); 
            }
            else
            {
                panelsSeen = true; 
                ResumeGame(); 
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

        if (other.CompareTag("Player") && !panelsSeen) 
        {
            Debug.Log("Player entered trigger zone. Showing bubble speech panels.");
            isPlayerNearby = true;

            if (bubbleSpeechPanels.Length > 0 && currentPanelIndex < bubbleSpeechPanels.Length)
            {
                bubbleSpeechPanels[currentPanelIndex].SetActive(true);
            }

            PauseGame(); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone. Hiding bubble speech panels.");
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

        currentPanelIndex = 0; 
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
