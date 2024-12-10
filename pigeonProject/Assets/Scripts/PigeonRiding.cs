using UnityEngine;

public class PigeonRiding : MonoBehaviour
{
    private Transform originalParent; // Store the original parent of the pigeon

    void Start()
    {
        // Save the original parent to reset later if needed
        originalParent = transform.parent;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the pigeon lands on the car
        if (collision.gameObject.CompareTag("Car"))
        {
            // Make the pigeon a child of the car
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the pigeon leaves the car
        if (collision.gameObject.CompareTag("Car"))
        {
            // Reset the pigeon to its original parent
            transform.SetParent(originalParent);
        }
    }
}
