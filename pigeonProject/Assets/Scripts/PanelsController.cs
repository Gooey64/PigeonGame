using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelsController : MonoBehaviour
{
    public GameObject panels; 
    private bool gamePaused = true;

    void Start()
    {
        if (panels != null)
        {
            panels.SetActive(true); 
            Debug.Log("Panels are now visible at the start of the game.");
        }

        PauseGame(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
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
            panels.SetActive(true); 
        }

        Debug.Log("Game paused. Panels are visible.");
    }

    public void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1;

        if (panels != null)
        {
            panels.SetActive(false); 
        }

        Debug.Log("Game resumed. Panels are hidden.");
    }
}
