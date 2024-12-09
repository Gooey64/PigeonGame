using UnityEngine;

public class RobotEnemy : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float fireRate = 0.5f; 
    public float projectileSpeed = 5f;

    public float moveSpeed = 2f; 
    public float moveRange = 5f; 

    private GameObject player; 
    private float nextFireTime = 0f;
    private float startX; 
    private bool movingRight = true; 

    void Start()
    {
        player = GameObject.FindWithTag("Player"); 
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the Player GameObject has the 'Player' tag.");
        }

        startX = transform.position.x; 
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Debug.Log("Shooting...");
            Shoot();
            nextFireTime = Time.time + fireRate; 
        }

        Move();
    }

    void Move()
    {
        float newX = transform.position.x + (movingRight ? moveSpeed : -moveSpeed) * Time.deltaTime;

        if (Mathf.Abs(newX - startX) >= moveRange)
        {
            movingRight = !movingRight;
        }

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Shoot()
    {
        if (player == null)
        {
            Debug.Log("No player found!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Debug.Log("Projectile instantiated at: " + firePoint.position);

        FollowPlayerScript followScript = projectile.AddComponent<FollowPlayerScript>();
        followScript.target = player.transform;
        followScript.speed = projectileSpeed;

        Debug.Log("FollowPlayerScript added to projectile, target assigned.");

        Destroy(projectile, 5f); 
    }
}
