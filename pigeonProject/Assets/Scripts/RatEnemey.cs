using System.Collections;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    public Transform pointA; // Patrol start point
    public Transform pointB; // Patrol end point
    public float patrolSpeed = 2f; // Speed during patrol
    public float chaseSpeed = 4f; // Speed while chasing
    public float detectionRange = 5f; // Distance to detect the player
    public Transform player; // Reference to the player

    private Vector3 targetPosition;
    private bool chasingPlayer = false;
    private bool isPaused = false;
    private bool isIdle = false;

    private Rigidbody2D rb; // For smooth physics-based movement

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = pointA.position; // Start by moving toward pointA
        StartCoroutine(PatrolPauseRoutine()); // Random pauses during patrol
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPaused || isIdle) return; // Stop movement during pause or idle

        // Check player distance for chasing
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        chasingPlayer = distanceToPlayer <= detectionRange;

        // Handle movement
        if (chasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Move smoothly towards the patrol target
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * patrolSpeed;

        // Switch patrol target when reaching close to current target
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    void ChasePlayer()
    {
        // Move smoothly towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
    }

    IEnumerator PatrolPauseRoutine()
    {
        while (true)
        {
            // Pause occasionally during patrol
            yield return new WaitForSeconds(Random.Range(3f, 6f)); // Random patrol time
            isIdle = true; // Make the rat idle
            rb.velocity = Vector3.zero; // Stop movement
            yield return new WaitForSeconds(Random.Range(1f, 2f)); // Random idle time
            isIdle = false; // Resume patrol
        }
    }

    public void Pause()
    {
        isPaused = true;
        rb.velocity = Vector3.zero; // Stop movement immediately
    }

    public void Resume()
    {
        isPaused = false;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize patrol points and detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
