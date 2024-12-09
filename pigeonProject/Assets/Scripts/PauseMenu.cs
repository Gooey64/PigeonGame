using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;       
    [SerializeField] private GameObject dimBackground;  
    public Button restartButton; 

    public static bool restartClicked = false; 

    private void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
    }

    private void OnRestartButtonClicked()
    {
        Debug.Log("Restart button clicked.");
        restartClicked = true; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        Time.timeScale = 1; 
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        if (dimBackground != null)
        {
            dimBackground.SetActive(true);
        }
        Time.timeScale = 0; 
    }

    public void Home() 
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1; 
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        if (dimBackground != null)
        {
            dimBackground.SetActive(false);
        }
        Time.timeScale = 1; 
    }

     public void Restart()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (dimBackground != null)
        {
            dimBackground.SetActive(false);
        }

        PanelManager panelManager = FindObjectOfType<PanelManager>();
        if (panelManager != null)
        {
            panelManager.OnGameRestart();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        Time.timeScale = 1; 
    }

    public static bool IsRestartClicked()
    {
        return restartClicked;
    }
}
