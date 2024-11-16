using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public float healthAmount = 100f;
    public Image healthBar; 

    void Update()
    {
        if (healthAmount <= 0) 
        {
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Heal(5);
        }
    }

    private void OnMouseDown()
    {
        TakeDamage(10); 
    }

    public void TakeDamage(float damage)
    {
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
        healthBar.fillAmount = healthAmount / 100f; 
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}