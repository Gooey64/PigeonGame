using UnityEngine;

public class Campfire : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f; // Amount of damage to deal

    void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log($"Trigger entered by: {other.name}");
    if (other.CompareTag("Player")) // Assuming your pigeon has the tag "Player"
    {
        Debug.Log("Player detected!");
        HealthManager healthManager = other.GetComponent<HealthManager>();

        if (healthManager != null)
        {
            Debug.Log("HealthManager found. Applying damage.");
            healthManager.TakeDamage(damageAmount);
        }
        else
        {
            Debug.LogError("HealthManager not found on the Player!");
        }
    }
}

}
