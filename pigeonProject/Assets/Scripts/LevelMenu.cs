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
        Debug.Log("HERE:");
        Debug.Log(ScoreManager.Instance.HasReachedTargetScore());
        if (ScoreManager.Instance.HasReachedTargetScore())
        {
            ShowWinPanel(); 
        }
        else
        {
            HideWinPanel(); 
        }
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); 
        }

        PauseGame(); 
    }

    private void HideWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false); 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && winPanel.activeSelf)
        {
            ProceedToNextLevel(); 
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
            Debug.Log("I AM HEREEEEEE");
            SceneManager.LoadScene(nextSceneIndex);
            ScoreManager.Instance.score = 0;
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
