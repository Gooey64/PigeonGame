using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public float healthAmount = 100f;
    public Image healthBar; 

    void Update()
    {
        if (healthAmount <= 0) 
        {
            Debug.Log(clip);
            SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1);
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            TakeDamage(20);
        }

    }

    public void TakeDamage(float damage)
    {
        healthAmount -= 30;
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
        healthBar.fillAmount = healthAmount / 100f; 
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
