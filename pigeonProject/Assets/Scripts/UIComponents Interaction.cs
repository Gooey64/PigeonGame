using UnityEngine;

public class UIComponentsInteraction : MonoBehaviour
{
    public GameObject[] bubbleSpeechPanels; // Array of bubble speech panels
    public GameObject[] bubbleSpeechImages; // Array of images corresponding to panels
    public GameObject[] bubbleSpeechIcons; // New array for additional icons corresponding to panels
    public SoundMixerManager soundMixerManager;

    private int currentPanelIndex = 0; // Tracks the current panel
    private bool isPlayerNearby = false;
    private bool isPickedUp = false;
    private bool gamePaused = false;
    private bool hasPanelDisplayed = false;

    private void Start()
    {
        DeactivateAll(bubbleSpeechPanels);
        DeactivateAll(bubbleSpeechImages);
        DeactivateAll(bubbleSpeechIcons);
    }

    private void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            HideAllPanelsAndImages();
            return;
        }

        if (isPlayerNearby && gamePaused && 
            (Input.GetButtonDown("Action") || 
             Input.GetKeyDown(KeyCode.RightArrow) || 
             Input.GetKeyDown(KeyCode.D)))
        {
            ShowNextUISet();
        }
    }

    private void ShowNextUISet()
    {
        if (currentPanelIndex < bubbleSpeechPanels.Length)
        {
            SetActive(bubbleSpeechPanels, currentPanelIndex, false);
            SetActive(bubbleSpeechImages, currentPanelIndex, false);
            SetActive(bubbleSpeechIcons, currentPanelIndex, false);

            currentPanelIndex++;

            if (currentPanelIndex < bubbleSpeechPanels.Length)
            {
                SetActive(bubbleSpeechPanels, currentPanelIndex, true);
                SetActive(bubbleSpeechImages, currentPanelIndex, true);
                SetActive(bubbleSpeechIcons, currentPanelIndex, true);
            }
            else
            {
                ResumeGame(); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            HideAllPanelsAndImages();
            return;
        }

        if (other.CompareTag("Player") && !isPickedUp && !hasPanelDisplayed)
        {
            Debug.Log("Player entered trigger zone: Showing bubble speech panels, images, and icons.");
            isPlayerNearby = true;

            SetActive(bubbleSpeechPanels, currentPanelIndex, true);
            SetActive(bubbleSpeechImages, currentPanelIndex, true);
            SetActive(bubbleSpeechIcons, currentPanelIndex, true);

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

            HideAllPanelsAndImages();
            ResumeGame();
        }
    }

    private void HideAllPanelsAndImages()
    {
        DeactivateAll(bubbleSpeechPanels);
        DeactivateAll(bubbleSpeechImages);
        DeactivateAll(bubbleSpeechIcons);

        currentPanelIndex = 0; 
    }

    private void DeactivateAll(GameObject[] array)
    {
        foreach (var obj in array)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void SetActive(GameObject[] array, int index, bool state)
    {
        if (index < array.Length && array[index] != null)
        {
            array[index].SetActive(state);
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
