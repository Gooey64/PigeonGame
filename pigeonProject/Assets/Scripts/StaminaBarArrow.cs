using UnityEngine;

public class StaminaBarArrow : MonoBehaviour
{
    public RectTransform staminaBar; // Reference to the stamina bar's RectTransform
    public RectTransform arrow;      // Reference to the arrow's RectTransform
    public Vector2 offset;           // Offset to adjust the arrow position relative to the stamina bar

    void Start()
    {
        // Ensure the arrow is visible at the start
        if (arrow != null)
        {
            arrow.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (staminaBar != null && arrow != null)
        {
            // Position the arrow relative to the stamina bar with the specified offset
            arrow.position = staminaBar.position + (Vector3)offset;
        }
    }
}
