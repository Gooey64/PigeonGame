using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneVertical : MonoBehaviour
{
    [SerializeField] private AudioClip drone;
    public float distance;
    float min;
    float max;
    bool up;
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.y - distance;
        max = transform.position.y + distance;
        up = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= min)
        {
            up = true;
        }
        else if (transform.position.y >= max)
        {
            up = false;
        }

        if (up)
        {
            transform.position += Vector3.up * 0.03f;
        }
        else
        {
            transform.position += Vector3.down * 0.03f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SoundFXManager.instance.PlaySoundFXClip(drone);
        if (other.CompareTag("Player"))
        {
            Debug.Log("DroneV hit the pigeon!");

            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }

        }
    }
}
