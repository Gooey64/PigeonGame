using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;       
    [SerializeField] private GameObject dimBackground;  

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
