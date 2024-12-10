using UnityEngine;

public class PigeonPlatformStick : MonoBehaviour
{
    private Transform originalParent; // To store the original parent of the pigeon
    private bool isOnPlatform = false;

    void Start()
    {
        // Save the original parent of the pigeon
        originalParent = transform.parent;
    }

    void Update()
    {
        // If the pigeon is on a platform and tries to fly or walk away
        if (isOnPlatform && (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") != 0))
        {
            DetachFromPlatform();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            AttachToPlatform(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            DetachFromPlatform();
        }
    }

    private void AttachToPlatform(Transform platform)
    {
        // Make the pigeon a child of the platform
        transform.SetParent(platform);
        isOnPlatform = true;
    }

    private void DetachFromPlatform()
    {
        // Reset the parent to the original parent
        transform.SetParent(originalParent);
        isOnPlatform = false;
    }
}
