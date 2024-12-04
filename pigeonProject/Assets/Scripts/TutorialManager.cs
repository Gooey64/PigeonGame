// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class TutorialManager : MonoBehaviour
// {
//     [SerializeField] private GameObject[] tutorialPanels;
//     private int currentPanelIndex = 0;
//     public GameObject player;
//     private pigeonMove pigeonMovement;
//     public Button nextButton;
//     public static bool tutorialCompleted = false;

//     private bool awaitingKeyPress = false;

//     private void Start()
//     {
//         if (SceneManager.GetActiveScene().name != "Level 1")
//         {
//             tutorialCompleted = true; 
//             return;
//         }

//         pigeonMovement = player.GetComponent<pigeonMove>();
//         if (pigeonMovement != null)
//         {
//             pigeonMovement.isAlive = false;
//         }

//         ShowCurrentPanel();
//     }

//     private void Update()
//     {
//         if (awaitingKeyPress)
//         {
//             // Check for movement key press
//             if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
//                 Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
//             {
//                 awaitingKeyPress = false;
//                 ShowNextPanel();
//             }
//         }
//     }

//     public void ShowNextPanel()
//     {
//         if (currentPanelIndex < tutorialPanels.Length)
//         {
//             tutorialPanels[currentPanelIndex].SetActive(false);
//             currentPanelIndex++;

//             if (currentPanelIndex < tutorialPanels.Length)
//             {
//                 ShowCurrentPanel();
//             }
//             else
//             {
//                 EndTutorial();
//             }
//         }
//     }

//     private void ShowCurrentPanel()
//     {
//         tutorialPanels[currentPanelIndex].SetActive(true);
//         if (currentPanelIndex == 0)
//         {
//             awaitingKeyPress = true;
//         }
//         else
//         {
//             awaitingKeyPress = false;
//         }
//     }

//     private void EndTutorial()
//     {
//         if (pigeonMovement != null)
//         {
//             pigeonMovement.isAlive = true;
//         }

//         tutorialCompleted = true;

//         Timer timer = FindObjectOfType<Timer>();
//         if (timer != null)
//         {
//             timer.StartTimer();
//         }
//     }
// }
