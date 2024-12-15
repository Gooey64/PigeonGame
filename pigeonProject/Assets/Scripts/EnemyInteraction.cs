using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    public GameObject bubbleSpeech;

    private bool isPlayerNearby = false;
    private bool isPickedUp = false;
    private bool gamePaused = false;
    private bool hasPanelDisplayed = false;

    private void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            bubbleSpeech.SetActive(false);
            return;
        }

        if (gamePaused && (Input.GetKeyDown(KeyCode.RightArrow) || 
                           Input.GetButtonDown("Action") || 
                           Input.GetKeyDown(KeyCode.D)))
        {
            ResumeGame();
            bubbleSpeech.SetActive(false); 
        }

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            bubbleSpeech.SetActive(false);
            return;
        }

        if (other.CompareTag("Player") && !isPickedUp && !hasPanelDisplayed)
        {
            Debug.Log("Player entered trigger zone: Showing bubble speech and freezing game.");
            isPlayerNearby = true;
            bubbleSpeech.SetActive(true);
            PauseGame();
            hasPanelDisplayed = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone.");
            isPlayerNearby = false;
            bubbleSpeech.SetActive(false);
        }
    }
    private void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        Debug.Log("Game paused.");
    }

    private void ResumeGame()
    {
        gamePaused = false;
        Time.timeScale = 1; 
        Debug.Log("Game resumed.");
    }
}
