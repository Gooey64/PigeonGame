using UnityEngine;

public class BlueberryInteraction : MonoBehaviour
{
    public GameObject bubbleSpeech; 
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            Debug.Log("Player touched the blueberry: Showing bubble speech.");
            hasActivated = true; 
            bubbleSpeech.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the blueberry area: Hiding bubble speech.");
            bubbleSpeech.SetActive(false); 
        }
    }
}
