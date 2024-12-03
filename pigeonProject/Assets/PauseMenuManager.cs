using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu; 
    public GameObject dimBackground; 

    public void TogglePauseMenu()
    {
        bool isActive = pauseMenu.activeSelf;
        pauseMenu.SetActive(!isActive);
        dimBackground.SetActive(!isActive);
    }
}
