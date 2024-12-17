using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private AudioClip Santa;
    public float damage = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Projectile hit the pigeon!");

            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                SoundFXManager.instance.PlaySoundFXClip(Santa);
                healthManager.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
