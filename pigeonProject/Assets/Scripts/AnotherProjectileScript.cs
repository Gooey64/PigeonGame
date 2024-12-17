using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherProjectileScript : MonoBehaviour
{
    [SerializeField] private AudioClip Santa;
    public float damage = 10f;
    public float speed = 5f; 
    public Transform target; 

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("No player found! Projectile will not track.");
        }
    }

    private void Update()
    {
        if (target == null)
            return;

        Vector2 direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
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
