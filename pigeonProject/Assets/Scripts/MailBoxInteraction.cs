using UnityEngine;

public class MailboxInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package")) // Adjust to match the tag on your envelope/package GameObject
        {
            if (ScoreManager.Instance.HasReachedTargetScore())
            {
                Debug.Log("Package successfully delivered to the mailbox.");
                Destroy(other.gameObject); // Destroy the package/envelope
            }
            else
            {
                Debug.Log("Not enough score to deliver the package!");
                // Optional: Add feedback to inform the player
            }
        }
    }
}
