using UnityEngine;

public class PigeonMovementWithPanel : MonoBehaviour
{
    public GameObject panel; 
    private bool gamePaused = true; 

    void Start()
    {
        Time.timeScale = 0;
        Debug.Log("Game paused.");
    }

    void Update()
    {
        if (gamePaused && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                           Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            ResumeGame();
        }
    }

    void ResumeGame()
    {
        if (panel != null)
        {
            panel.SetActive(false); 
        }

        Time.timeScale = 1; 
        gamePaused = false; 
        Debug.Log("Game resumed.");
    }
}
