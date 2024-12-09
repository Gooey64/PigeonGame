using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float projectileSpeed = 10f;
    public Transform target;

    public float moveSpeed = 5f;
    public float verticalOffset = 2f;

    private float fireCooldown = 0f;

    void Update()
    {
        if (target != null)
        {
            FollowTargetWithOffset();
            ShootAtIntervals();
        }
    }

    void FollowTargetWithOffset()
    {
        Vector3 targetPosition = new Vector3(
            target.position.x, 
            target.position.y + verticalOffset, 
            transform.position.z
        );
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void ShootAtIntervals()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void Shoot()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
        Destroy(projectile, 5f);
    }
}
