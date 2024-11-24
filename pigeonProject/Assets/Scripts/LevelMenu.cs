using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public Color unlockedColor = Color.white; 
    public Color lockedColor = new Color(1f, 1f, 1f, 0.5f); 

    private void Awake()
    {
        UpdateLevelButtons();
        Debug.Log($"Loaded Scene: {SceneManager.GetActiveScene().name}, Index: {SceneManager.GetActiveScene().buildIndex}");
    }

    private void ResetLevels()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            PlayerPrefs.SetInt($"Level_{i}_Unlocked", i == 0 ? 1 : 0); 
        }
        PlayerPrefs.Save();
        Debug.Log("Game reset: Only Level 0 is unlocked.");
    }

    private void UpdateLevelButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (PlayerPrefs.GetInt($"Level_{i}_Unlocked", 0) == 1)
            {
                buttons[i].interactable = true;
                buttons[i].GetComponent<Image>().color = unlockedColor;
            }
            else
            {
                buttons[i].interactable = false;
                buttons[i].GetComponent<Image>().color = lockedColor;
            }
        }
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level " + levelID; 
        SceneManager.LoadScene(levelName);
    }

    public void PlayGame()
    {
        ResetLevels();
        Debug.Log("Starting at Level 0");
        SceneManager.LoadScene(1); 
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0); 
    }

    public void LoadNextLevel()
    {
        Debug.Log("Next Level button clicked!");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"Current Scene Index: {currentSceneIndex}");

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            int nextSceneIndex = currentSceneIndex;
            Debug.Log($"Loading Next Level: {nextSceneIndex}");

            UnlockNextLevel(nextSceneIndex);

            SceneManager.LoadScene(nextSceneIndex + 1);
        }
        else
        {
            Debug.Log("No more levels to load!");
        }
    }

    public void UnlockNextLevel(int nextLevelIndex)
    {
        Debug.Log($"Attempting to unlock Level {nextLevelIndex}");

        if (PlayerPrefs.GetInt($"Level_{nextLevelIndex}_Unlocked", 0) == 0)
        {
            PlayerPrefs.SetInt($"Level_{nextLevelIndex}_Unlocked", 1);
            PlayerPrefs.Save();
            Debug.Log($"Level {nextLevelIndex} unlocked.");
        }
        else
        {
            Debug.Log($"Level {nextLevelIndex} already unlocked.");
        }

        if (nextLevelIndex < buttons.Length)
        {
            buttons[nextLevelIndex].interactable = true;
            buttons[nextLevelIndex].GetComponent<Image>().color = unlockedColor;
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
