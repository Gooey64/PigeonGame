using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelsController : MonoBehaviour
{
    public GameObject panels; // Assign your panels GameObject in the Inspector
    private bool gamePaused = true;

    void Start()
    {
        if (panels != null)
        {
            panels.SetActive(true); // Ensure the panels are visible at the start
            Debug.Log("Panels are now visible at the start of the game.");
        }

        PauseGame(); // Start the game in a paused state
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Example pause/unpause toggle key
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;

        if (panels != null)
        {
            panels.SetActive(true); // Ensure panels remain visible during pause
        }

        Debug.Log("Game paused. Panels are visible.");
    }

    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;

        if (panels != null)
        {
            panels.SetActive(false); // Hide panels when the game resumes
        }

        Debug.Log("Game resumed. Panels are hidden.");
    }
}
