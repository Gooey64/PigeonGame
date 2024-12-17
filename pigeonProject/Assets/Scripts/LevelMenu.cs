using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons; 
    public GameObject winPanel; 
    public Color unlockedColor = Color.white;
    public Color lockedColor = new Color(1f, 1f, 1f, 0.5f);

    private bool isPaused = false;

    private void Awake()
    {
        UpdateLevelButtons();
        CheckAndShowWinPanel(); 
        Debug.Log($"Loaded Scene: {SceneManager.GetActiveScene().name}, Index: {SceneManager.GetActiveScene().buildIndex}");
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == 0 || PlayerPrefs.GetInt($"Level_{i}_Unlocked", 0) == 1)
            {
                buttons[i].interactable = true;
                buttons[i].GetComponent<Image>().color = unlockedColor;
            }
            else
            {
                buttons[i].interactable = false; 
                buttons[i].GetComponent<Image>().color = lockedColor;
            }

            int levelIndex = i;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OpenLevel(levelIndex));
        }
    }

    private void CheckAndShowWinPanel()
    {
        if (ScoreManager.Instance.HasReachedTargetScore())
        {
            ShowWinPanel(); // Show the win panel
        }
        else
        {
            HideWinPanel(); // Hide the win panel if conditions are not met
        }
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); // Show the panel
        }

        PauseGame(); // Pause the game to let the player view the message
    }

    private void HideWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false); // Hide the panel
        }
    }

    private void Update()
    {
        // Check if the right arrow is pressed and the win panel is active
        if (Input.GetKeyDown(KeyCode.RightArrow) && winPanel.activeSelf)
        {
            ProceedToNextLevel(); // Proceed to the next level
        }
    }

    private void ProceedToNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            int nextSceneIndex = currentSceneIndex + 1;
            UnlockNextLevel(nextSceneIndex);
            ResumeGame();
            SceneManager.LoadScene(nextSceneIndex); // Load the next level
        }
        else
        {
            Debug.Log("No more levels to load!");
        }
    }

    private void UnlockNextLevel(int nextLevelIndex)
    {
        if (PlayerPrefs.GetInt($"Level_{nextLevelIndex}_Unlocked", 0) == 0)
        {
            PlayerPrefs.SetInt($"Level_{nextLevelIndex}_Unlocked", 1);
            PlayerPrefs.Save();
        }

        if (nextLevelIndex < buttons.Length)
        {
            buttons[nextLevelIndex].interactable = true;
            buttons[nextLevelIndex].GetComponent<Image>().color = unlockedColor;
        }
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level " + levelID;
        ResumeGame();
        SceneManager.LoadScene(levelName);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            Debug.Log("Game Paused!");
        }
    }

    public void ResumeGame()
    {
        if (isPaused || Time.timeScale == 0)
        {
            isPaused = false;
            Time.timeScale = 1;
            Debug.Log("Game Resumed!");
        }
    }

    [ContextMenu("Clear PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared.");
    }
}
