using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject panel; 
    private static bool gameRestartedOrDied = false; 

    void Start()
    {
        if (!gameRestartedOrDied)
        {
            Time.timeScale = 0; 
            if (panel != null)
            {
                panel.SetActive(true); 
            }
        }
        else
        {
            Time.timeScale = 1;
            if (panel != null)
            {
                panel.SetActive(false); 
            }
            gameRestartedOrDied = false; 
        }
    }

    void Update()
    {
        if (HealthManager.PlayerDied)
        {
            HandlePlayerDeath();
        }
    }

    public void OnGameRestart()
    {
        gameRestartedOrDied = true; 

        if (panel != null)
        {
            panel.SetActive(false); 
        }
    }

    private void HandlePlayerDeath()
    {
        gameRestartedOrDied = true;

        if (panel != null)
        {
            panel.SetActive(false); 
        }

        Time.timeScale = 1;
        Debug.Log("Player died. Game resumed.");
    }
}
