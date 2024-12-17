using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeDelivery : MonoBehaviour
{
    public Rigidbody2D envelopeRigidbody; // Rigidbody of the envelope
    public GameObject envelope; // The envelope GameObject
    public Transform groundPosition; // The position where the envelope falls if delivery fails

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Envelope")) // Check if the envelope interacts with the mailbox
        {
            if (ScoreManager.Instance.HasReachedTargetScore())
            {
                DeliverEnvelope(other.gameObject);
            }
            else
            {
                DropEnvelopeToGround(other.gameObject);
            }
        }
    }

    private void DeliverEnvelope(GameObject envelope)
    {
        Debug.Log("Envelope successfully delivered!");
        Destroy(envelope); // Destroy the envelope GameObject
    }

    private void DropEnvelopeToGround(GameObject envelope)
    {
        Debug.Log("Target score not reached. Envelope dropped to the ground.");

        Rigidbody2D envelopeRb = envelope.GetComponent<Rigidbody2D>();
        if (envelopeRb != null)
        {
            envelopeRb.bodyType = RigidbodyType2D.Dynamic; // Enable gravity to let it fall naturally
        }

        // Position the envelope at the ground position
        envelope.transform.position = groundPosition.position;
    }
}
