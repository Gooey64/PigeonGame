using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHorizonal : MonoBehaviour
{
    [SerializeField] private AudioClip drone;
    public float distance;
    float min;
    float max;
    bool right;
   public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x - distance;
        max = transform.position.x + distance;
        right = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= min)
        {
            right = true;
        }
        else if (transform.position.x >= max)
        {
            right = false;
        }

        if (right)
        {
            transform.position += Vector3.right * 0.04f;
        }
        else
        {
            transform.position += Vector3.left * 0.04f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundFXManager.instance.PlaySoundFXClip(drone);
            Debug.Log("DroneH hit the pigeon!");

            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }

        }
    }
}

