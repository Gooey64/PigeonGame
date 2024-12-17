using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvelopeDelivery : MonoBehaviour
{
    public Rigidbody2D envelopeRigidbody; 
    public GameObject envelope; 
    public Transform groundPosition; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Envelope")) 
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
        Destroy(envelope); 
    }

    private void DropEnvelopeToGround(GameObject envelope)
    {
        Debug.Log("Target score not reached. Envelope dropped to the ground.");

        Rigidbody2D envelopeRb = envelope.GetComponent<Rigidbody2D>();
        if (envelopeRb != null)
        {
            envelopeRb.bodyType = RigidbodyType2D.Dynamic;
        }
        envelope.transform.position = groundPosition.position;
    }
}
