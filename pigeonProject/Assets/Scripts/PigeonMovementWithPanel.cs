using UnityEngine;
using System.Collections;

public class PigeonMovementWithPanel : MonoBehaviour
{
    public GameObject[] initialPanels; 
    public GameObject[] envelopePanels; 
    public GameObject[] arrows; 
    public GameObject[] objects; 
    private int currentPanelIndex = 0; 
    private bool gamePaused = true; 
    private bool envelopePickedUp = false; 
    private bool inSecondPanelArray = false; 
    private bool isTransitioning = false; 

    void Start()
    {
        Time.timeScale = 0; 
        Debug.Log("Game paused.");

        for (int i = 0; i < initialPanels.Length; i++)
        {
            initialPanels[i].SetActive(i == 0);
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

        if (gamePaused && inSecondPanelArray &&  (Input.GetKeyDown(KeyCode.RightArrow)   || 
                                   Input.GetButtonDown("Action") || 
                                   Input.GetKeyDown(KeyCode.D)))
        {
            envelopePanels[0].SetActive(false);
            ResumeGame();
            TraverseEnvelopePanels();
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
        }
    }

    void PickUpEnvelope()
    {
        envelopePickedUp = true;
        Debug.Log("Envelope picked up. Showing second panel array after 1.5 seconds.");
        StartCoroutine(ShowEnvelopePanelsAfterDelay());
    }

    IEnumerator ShowEnvelopePanelsAfterDelay()
    {
        yield return WaitForTravelDistance(20f); 

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

    void TraverseEnvelopePanels()
    {
        if (!isTransitioning && currentPanelIndex < envelopePanels.Length)
        {
            StartCoroutine(ShowNextPanelWithDelay());
        }
    }

    IEnumerator ShowNextPanelWithDelay()
    {
        isTransitioning = true; 

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

        isTransitioning = false;
    }

    IEnumerator WaitForTravelDistance(float targetDistance)
    {
        float traveledDistance = 0f;
        Vector3 initialPosition = transform.position;

        while (traveledDistance < targetDistance)
        {
            traveledDistance = Vector3.Distance(transform.position, initialPosition);
            yield return null; // Wait for the next frame
        }
    }

    void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        Debug.Log("Game paused.");
    }

    void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        Debug.Log("Game resumed.");
    }
}