using System.Collections;
using UnityEngine;

public class RatEnemy : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB; 
    public float patrolSpeed = 2f; 
    public float chaseSpeed = 4f; 
    public float detectionRange = 5f; 
    public Transform player; 

    private Vector3 targetPosition;
    private bool chasingPlayer = false;
    private bool isPaused = false;
    private bool isIdle = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = pointA.position; 
        StartCoroutine(PatrolPauseRoutine()); 
    }

    void FixedUpdate()
    {
        if (isPaused || isIdle) return; 

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        chasingPlayer = distanceToPlayer <= detectionRange;

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
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * patrolSpeed;

        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
    }

    IEnumerator PatrolPauseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f)); 
            isIdle = true;
            rb.velocity = Vector3.zero; 
            yield return new WaitForSeconds(Random.Range(1f, 2f)); 
            isIdle = false; 
        }
    }

    public void Pause()
    {
        isPaused = true;
        rb.velocity = Vector3.zero; 
    }

    public void Resume()
    {
        isPaused = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
    }
}
