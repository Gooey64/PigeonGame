using UnityEngine;

public class EnvelopeInteraction : MonoBehaviour
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

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            TryPickUpEnvelope();
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

    private void TryPickUpEnvelope()
    {
        if (PlayerHasPickedUpEnvelope())
        {
            Debug.Log("Envelope successfully picked up!");
            isPickedUp = true;
            bubbleSpeech.SetActive(false);
            ResumeGame(); 
            Destroy(gameObject); 
        }
        else
        {
            Debug.Log("Failed to pick up envelope.");
        }
    }

    private bool PlayerHasPickedUpEnvelope()
    {
        return true;
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
