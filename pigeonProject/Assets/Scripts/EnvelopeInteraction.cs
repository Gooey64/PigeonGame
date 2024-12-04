using UnityEngine;

public class EnvelopeInteraction : MonoBehaviour
{
    public GameObject bubbleSpeech; 
    private bool isPlayerNearby = false;
    private bool isPickedUp = false; 
    private bool isLeftArrowPressed = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isLeftArrowPressed = true;
            if (bubbleSpeech.activeSelf)
            {
                bubbleSpeech.SetActive(false); 
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isLeftArrowPressed = false;
        }

      
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            TryPickUpEnvelope();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp && !isLeftArrowPressed)
        {
            Debug.Log("Player entered trigger zone.");
            isPlayerNearby = true;
            bubbleSpeech.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger zone.");
            isPlayerNearby = false;
            bubbleSpeech.SetActive(false); 
        }
    }

    private void TryPickUpEnvelope()
    {
        if (PlayerHasPickedUpEnvelope()) 
        {
            Debug.Log("Envelope successfully picked up!");
            isPickedUp = true;
            bubbleSpeech.SetActive(false); 
            Destroy(gameObject); 
        }
        else
        {
            Debug.Log("Failed to pick up envelope.");
        }
    }

    private bool PlayerHasPickedUpEnvelope()
    {
        return true; 
    }
}
