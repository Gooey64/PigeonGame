using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip scorenoise;
    public int scoreValue = 10; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Collectible picked up!");
            SoundFXManager.instance.PlaySoundFXClip(scorenoise);

            ScoreManager.Instance.AddScore(scoreValue);

            Destroy(gameObject); 
        }
    }
}
