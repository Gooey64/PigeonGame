using UnityEngine;

public class BlueberryInteraction : MonoBehaviour
{
    public GameObject bubbleSpeech; // Reference to the bubble speech GameObject
    private bool hasActivated = false; // Track if the bubble speech has already been shown

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            Debug.Log("Player touched the blueberry: Showing bubble speech.");
            hasActivated = true; // Ensure this runs only once
            bubbleSpeech.SetActive(true); // Show the bubble speech
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the blueberry area: Hiding bubble speech.");
            bubbleSpeech.SetActive(false); // Hide the bubble speech
        }
    }
}
