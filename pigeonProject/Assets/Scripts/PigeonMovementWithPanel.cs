using UnityEngine;
using System.Collections;

public class PigeonMovementWithPanel : MonoBehaviour
{
    public GameObject[] initialPanels;
    public GameObject[] envelopePanels;
    public GameObject[] arrows;
    public GameObject[] objects;

    public SoundMixerManager soundMixerManager;
    private int currentPanelIndex = 0;
    private bool gamePaused = true;
    private bool envelopePickedUp = false;
    private bool inSecondPanelArray = false;
    private bool isTransitioning = false;

    void Start()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            Debug.Log("Player has died or level restarted. Disabling all panels and arrays.");
            DisableAllPanelsAndArrays();
            return;
        }

        if (initialPanels.Length > 0)
        {
            Time.timeScale = 0;
            Debug.Log("Game paused.");

            for (int i = 0; i < initialPanels.Length; i++)
            {
                initialPanels[i].SetActive(i == 0);
            }
        }
        else
        {
            Debug.LogWarning("No initial panels found. Skipping initial panel setup.");
            ResumeGame();
        }

        foreach (var arrow in arrows)
        {
            if (arrow != null)
            {
                arrow.SetActive(false);
            }
        }

        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            DisableAllPanelsAndArrays();
            return;
        }

        if (gamePaused && (Input.GetKeyDown(KeyCode.RightArrow) ||
                           Input.GetButtonDown("Action") ||
                           Input.GetKeyDown(KeyCode.D)))
        {
            HideCurrentPanel();
        }

        if (!envelopePickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUpEnvelope();
        }

        if (gamePaused && inSecondPanelArray && (Input.GetKeyDown(KeyCode.RightArrow) ||
                                                 Input.GetButtonDown("Action") ||
                                                 Input.GetKeyDown(KeyCode.D)))
        {
            if (envelopePanels.Length > 0)
            {
                envelopePanels[0].SetActive(false);
                ResumeGame();
                TraverseEnvelopePanels();
            }
            else
            {
                Debug.LogWarning("No envelope panels found. Cannot proceed.");
            }
        }
    }

    void HideCurrentPanel()
    {
        if (!envelopePickedUp)
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
    }

    void PickUpEnvelope()
    {
        if (envelopePanels.Length > 0)
        {
            envelopePickedUp = true;
            Debug.Log("Envelope picked up. Showing second panel array after 1.5 seconds.");
            StartCoroutine(ShowEnvelopePanelsAfterDelay());
        }
        else
        {
            Debug.LogWarning("No envelope panels available. Skipping.");
        }
    }

    IEnumerator ShowEnvelopePanelsAfterDelay()
    {
        yield return WaitForTravelDistance(20f);

        if (envelopePanels.Length > 0)
        {
            currentPanelIndex = 0;
            inSecondPanelArray = true;

            for (int i = 0; i < envelopePanels.Length; i++)
            {
                envelopePanels[i].SetActive(i == 0);
            }

            if (arrows.Length > 0 && arrows[0] != null)
            {
                arrows[0].SetActive(true);
            }
            if (objects.Length > 0 && objects[0] != null)
            {
                objects[0].SetActive(true);
            }

            PauseGame();

            Debug.Log("Second panel array is now visible after 1.5 seconds.");
        }
        else
        {
            Debug.LogWarning("No envelope panels to display.");
        }
    }

    void TraverseEnvelopePanels()
    {
        if (!isTransitioning && currentPanelIndex < envelopePanels.Length)
        {
            StartCoroutine(ShowNextPanelWithDelay());
        }
        else
        {
            Debug.LogWarning("No more envelope panels to traverse.");
        }
    }

    IEnumerator ShowNextPanelWithDelay()
    {
        isTransitioning = true;

        if (currentPanelIndex < envelopePanels.Length)
        {
            envelopePanels[currentPanelIndex].SetActive(false);
            if (currentPanelIndex < arrows.Length && arrows[currentPanelIndex] != null)
            {
                arrows[currentPanelIndex].SetActive(false);
            }
            if (currentPanelIndex < objects.Length && objects[currentPanelIndex] != null)
            {
                objects[currentPanelIndex].SetActive(false);
            }

            yield return WaitForTravelDistance(20f);

            currentPanelIndex++;

            if (currentPanelIndex < envelopePanels.Length)
            {
                envelopePanels[currentPanelIndex].SetActive(true);

                if (currentPanelIndex < arrows.Length && arrows[currentPanelIndex] != null)
                {
                    arrows[currentPanelIndex].SetActive(true);
                }
                if (currentPanelIndex < objects.Length && objects[currentPanelIndex] != null)
                {
                    objects[currentPanelIndex].SetActive(true);
                }
                PauseGame();
                Debug.Log($"Panel {currentPanelIndex}, corresponding arrow, and object displayed.");
            }
            else
            {
                ResumeGame();
                inSecondPanelArray = false;
            }
        }
        else
        {
            Debug.LogWarning("No more panels available for transition.");
        }

        isTransitioning = false;
    }

    IEnumerator WaitForTravelDistance(float targetDistance)
    {
        float traveledDistance = 0f;
        Vector3 initialPosition = transform.position;

        while (traveledDistance < targetDistance)
        {
            traveledDistance = Vector3.Distance(transform.position, initialPosition);
            yield return null;
        }
    }

    void PauseGame()
    {
        if (initialPanels.Length == 0 && envelopePanels.Length == 0)
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

    void DisableAllPanelsAndArrays()
    {
        foreach (var panel in initialPanels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        foreach (var panel in envelopePanels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        foreach (var arrow in arrows)
        {
            if (arrow != null)
            {
                arrow.SetActive(false);
            }
        }

        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        Debug.Log("All panels and arrays have been disabled.");
    }
}
