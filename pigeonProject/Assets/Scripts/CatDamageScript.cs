using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDamageScript : MonoBehaviour
{

    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DroneH hit the pigeon!");

            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(damage);
            }

        }
    }
}
