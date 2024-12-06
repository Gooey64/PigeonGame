using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private AudioClip scorenoise;
    public int scoreValue = 10;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Collectible picked up!");
                SoundFXManager.instance.PlaySoundFXClip(scorenoise);
                ScoreManager.Instance.AddScore(scoreValue);
                Destroy(gameObject);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed but does not pick up the collectible.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in range to pick up the collectible.");
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the range of the collectible.");
            isPlayerInRange = false;
        }
    }
}
