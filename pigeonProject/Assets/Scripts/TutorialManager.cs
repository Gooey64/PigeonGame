using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialPanels;
    private int currentPanelIndex = 0;
    public GameObject player;
    private pigeonMove pigeonMovement;
    public Button nextButton;
    public static bool tutorialCompleted = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Level 1")
        {
            tutorialCompleted = true; 
            return;
        }

        pigeonMovement = player.GetComponent<pigeonMove>();
        if (pigeonMovement != null)
        {
            pigeonMovement.isAlive = false;
        }

        ShowCurrentPanel();
    }

    public void ShowNextPanel()
    {
        tutorialPanels[currentPanelIndex].SetActive(false);

        currentPanelIndex++;

        if (currentPanelIndex < tutorialPanels.Length)
        {
            ShowCurrentPanel();
        }
        else
        {
            if (pigeonMovement != null)
            {
                pigeonMovement.isAlive = true;
            }

            tutorialCompleted = true;

            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.StartTimer();
            }
        }
    }

    private void ShowCurrentPanel()
    {
        tutorialPanels[currentPanelIndex].SetActive(true);
    }
}
