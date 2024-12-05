using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip damageClip;
    public float healthAmount = 100f;
    public Image healthBar;

    public GameObject instructionsPanel1; // First instruction panel
    public GameObject instructionsPanel2; // Second instruction panel
    public GameObject instructionsPanel3; // Third instruction panel
    public Canvas instructionsCanvas; // Canvas to hide

    private void Start()
    {
        HandleReplayFlags();
    }

    void Update()
    {
        if (healthAmount <= 0) 
        {
            Debug.Log("Health depleted. Reloading level...");
            SoundFXManager.instance.PlaySoundFXClip(clip);
            RecordReplay();
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage)
    {
        SoundFXManager.instance.PlaySoundFXClip(damageClip);
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); 
        UpdateHealthBar();
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100); 
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / 100f; 
        }
    }

    private void ReloadLevel()
    {
        healthAmount = 100f;

        // Set replay flag in PlayerPrefs
        RecordReplay();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    private void RecordReplay()
    {
        PlayerPrefs.SetInt("LevelReplayed", 1); // Mark the level as being replayed
        PlayerPrefs.Save();
    }

    private void HandleReplayFlags()
    {
        // Check if the level is being replayed
        bool isReplaying = PlayerPrefs.GetInt("LevelReplayed", 0) == 1;

        Debug.Log($"Replay Flag: {isReplaying}");

        if (isReplaying)
        {
            // Disable all instruction panels and the canvas if it's a replay
            if (instructionsPanel1 != null) instructionsPanel1.SetActive(false);
            if (instructionsPanel2 != null) instructionsPanel2.SetActive(false);
            if (instructionsPanel3 != null) instructionsPanel3.SetActive(false);

            if (instructionsCanvas != null)
            {
                instructionsCanvas.gameObject.SetActive(false);
                Debug.Log("Instructions Canvas hidden on replay.");
            }
        }
        else
        {
            // First playthrough: Ensure all instruction panels and canvas are active
            if (instructionsPanel1 != null) instructionsPanel1.SetActive(true);
            if (instructionsPanel2 != null) instructionsPanel2.SetActive(true);
            if (instructionsPanel3 != null) instructionsPanel3.SetActive(true);

            if (instructionsCanvas != null)
            {
                instructionsCanvas.gameObject.SetActive(true);
                Debug.Log("Instructions Canvas displayed on first playthrough.");
            }

            // Reset replay flag for subsequent levels
            PlayerPrefs.SetInt("LevelReplayed", 0);
            PlayerPrefs.Save();
        }
    }

    [ContextMenu("Reset Replay Flag")]
    public void ResetReplayFlag()
    {
        PlayerPrefs.SetInt("LevelReplayed", 0);
        PlayerPrefs.Save();
        Debug.Log("Replay flag reset.");
    }

    [ContextMenu("Clear PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared.");
    }
}
