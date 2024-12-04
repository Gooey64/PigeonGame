using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 10; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Collectible picked up!");

            ScoreManager.Instance.AddScore(scoreValue);

            Destroy(gameObject); 
        }
    }
}
