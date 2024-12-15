using UnityEngine;
using System.Collections;

public class PigeonMovementWithPanel : MonoBehaviour
{
    public GameObject[] initialPanels; 
    public GameObject[] envelopePanels; 
    public GameObject[] arrows; 
    private int currentPanelIndex = 0; 
    private bool gamePaused = true; 
    private bool envelopePickedUp = false; 
    private bool inSecondPanelArray = false; 

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
    }

    void Update()
    {
        if (gamePaused && Input.GetKeyDown(KeyCode.RightArrow))
        {
            HideCurrentPanel();
        }

        if (!envelopePickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUpEnvelope();
        }

        if (inSecondPanelArray && Input.GetKeyDown(KeyCode.RightArrow))
        {
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
        Debug.Log("Envelope picked up. Showing second panel array after 5 seconds.");
        StartCoroutine(ShowEnvelopePanelsAfterDelay());
    }

    IEnumerator ShowEnvelopePanelsAfterDelay()
    {
        yield return new WaitForSeconds(5f); 

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

        Debug.Log("Second panel array is now visible.");
    }

    void TraverseEnvelopePanels()
{
    if (currentPanelIndex < envelopePanels.Length)
    {
        envelopePanels[currentPanelIndex].SetActive(false);
        if (currentPanelIndex < arrows.Length && arrows[currentPanelIndex] != null)
        {
            arrows[currentPanelIndex].SetActive(false);
            Debug.Log($"Hiding arrow for panel index {currentPanelIndex}, Arrow position: {arrows[currentPanelIndex].transform.position}");
        }

        currentPanelIndex++;

        if (currentPanelIndex < envelopePanels.Length)
        {
            envelopePanels[currentPanelIndex].SetActive(true);
            Debug.Log($"Showing panel index {currentPanelIndex}");

            if (currentPanelIndex < arrows.Length && arrows[currentPanelIndex] != null)
            {
                arrows[currentPanelIndex].SetActive(true);
                Debug.Log($"Showing arrow for panel index {currentPanelIndex}, Arrow position: {arrows[currentPanelIndex].transform.position}");
            }
        }
        else
        {
            ResumeGame(); 
            inSecondPanelArray = false;
        }
    }
}


    void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        Debug.Log("Game resumed.");
    }
}
