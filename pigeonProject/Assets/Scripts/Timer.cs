using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; 
    [SerializeField] private float remainingTime = 60f; 

    private bool isTimerRunning = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (TutorialManager.tutorialCompleted)
            {
                StartTimer();
            }
        }
        else
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (!isTimerRunning || timerText == null)
            return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 10 && timerText.color != Color.red)
            {
                timerText.color = Color.red;
            }
        }
        else
        {
            remainingTime = 0;
            isTimerRunning = false; 
            ResetLevel();
        }

        UpdateTimerText();
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime % 60F);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

