using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialPanels; // Array of tutorial panels
    private int currentPanelIndex = 0;
    public GameObject player; // Reference to the player GameObject
    private pigeonMove pigeonMovement; // Reference to the pigeonMove script
    public Button nextButton; // Button for advancing tutorial

    private void Start()
    {
        // Get the pigeonMove script from the player GameObject
        pigeonMovement = player.GetComponent<pigeonMove>();
        if (pigeonMovement != null)
        {
            pigeonMovement.isAlive = false; // Disable movement at the start of the tutorial
        }

        ShowCurrentPanel();
    }

    public void ShowNextPanel()
    {
        // Hide the current tutorial panel
        tutorialPanels[currentPanelIndex].SetActive(false);

        currentPanelIndex++;

        if (currentPanelIndex < tutorialPanels.Length)
        {
            ShowCurrentPanel(); // Show the next tutorial panel
        }
        else
        {
            // End of the tutorial, enable movement
            if (pigeonMovement != null)
            {
                pigeonMovement.isAlive = true;
            }
        }
    }

    private void ShowCurrentPanel()
    {
        tutorialPanels[currentPanelIndex].SetActive(true); // Display the current panel
    }
}
