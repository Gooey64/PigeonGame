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

    public GameObject instructionsPanel1;
    public GameObject instructionsPanel2;
    public GameObject instructionsPanel3;
    public Canvas instructionsCanvas;

    public static bool PlayerDied = false; 

    private SpriteRenderer[] spriteRenderers;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(spriteRenderers.Length);
       
        if (PlayerPrefs.GetInt("LevelReplayed", 0) == 0)
        {
            PlayerDied = false;
        }

        HandleReplayFlags();
    }

    void Update()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (healthAmount <= 0)
        {
            Debug.Log("Health depleted. Reloading level...");
            PlayerDied = true; // Mark the player as dead
            SoundFXManager.instance.PlaySoundFXClip(clip);
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
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        SpriteRenderer[] renderersAtTimeOfDamage = spriteRenderers;
        Debug.Log("We are supposed to flash red now");
        for (int i = 0; i < renderersAtTimeOfDamage.Length; i++)
        {
            renderersAtTimeOfDamage[i].color = Color.red;
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < renderersAtTimeOfDamage.Length; i++)
        {
            renderersAtTimeOfDamage[i].color = Color.white;
        }
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
        PlayerPrefs.SetInt("LevelReplayed", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleReplayFlags()
    {
        bool isReplaying = PlayerPrefs.GetInt("LevelReplayed", 0) == 1;

        if (isReplaying)
        {
            Debug.Log("Replay detected: Hiding instructions.");

            if (instructionsPanel1 != null) instructionsPanel1.SetActive(false);
            if (instructionsPanel2 != null) instructionsPanel2.SetActive(false);
            if (instructionsPanel3 != null) instructionsPanel3.SetActive(false);
            if (instructionsCanvas != null) instructionsCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("First playthrough: Showing instructions.");

            if (instructionsPanel1 != null) instructionsPanel1.SetActive(true);
            if (instructionsPanel2 != null) instructionsPanel2.SetActive(true);
            if (instructionsPanel3 != null) instructionsPanel3.SetActive(true);
            if (instructionsCanvas != null) instructionsCanvas.gameObject.SetActive(true);

            PlayerPrefs.SetInt("LevelReplayed", 0);
            PlayerPrefs.Save();
        }
    }
}
