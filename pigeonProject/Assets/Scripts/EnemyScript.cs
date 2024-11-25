using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float fireRate = 0.5f; 
    public float projectileSpeed = 5f;

    private GameObject player; 
    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
    }

    void Update()
    {
        if (TutorialManager.tutorialCompleted && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; 
        }
    }

    void Shoot()
    {
        if (player == null) return;

        Vector2 direction = (player.transform.position - firePoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed; 
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on the projectile!");
        }

        Destroy(projectile, 5f);
    }
}
