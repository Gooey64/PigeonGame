using UnityEngine;

public class AttachGameObjectOnDirection : MonoBehaviour
{
    public GameObject rightFacingObject; // Assign the GameObject for the right-facing direction
    public GameObject leftFacingObject; // Assign the GameObject for the left-facing direction
    private bool isFacingRight = true; // Track the car's facing direction

    void Update()
    {
        // Check the car's facing direction
        if (transform.localScale.x > 0 && !isFacingRight)
        {
            // Switch to right-facing object
            SetActiveObject(rightFacingObject, leftFacingObject);
            isFacingRight = true;
        }
        else if (transform.localScale.x < 0 && isFacingRight)
        {
            // Switch to left-facing object
            SetActiveObject(leftFacingObject, rightFacingObject);
            isFacingRight = false;
        }
    }

    void SetActiveObject(GameObject activeObject, GameObject inactiveObject)
    {
        if (activeObject != null && !activeObject.activeSelf)
        {
            activeObject.SetActive(true); // Activate the correct object
        }

        if (inactiveObject != null && inactiveObject.activeSelf)
        {
            inactiveObject.SetActive(false); // Deactivate the other object
        }
    }
}
