using UnityEngine;

public class SplineMovement : MonoBehaviour
{
    public Transform[] controlPoints; // Add control points in the Inspector
    public bool[] flipAtControlPoints; // Match flipping with control points
    public float speed = 0.5f; // Adjust the speed
    private float t = 0f; // Parameter to track movement along the spline
    private int previousSegment = -1;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found! Add a SpriteRenderer to the car.");
        }
    }

    void Update()
    {
        if (controlPoints.Length < 4) return;

        // Calculate the current segment and local t
        int segment = Mathf.FloorToInt(t);
        float localT = t - segment;

        // Get control points for Catmull-Rom spline
        Transform p0 = controlPoints[Clamp(segment - 1)];
        Transform p1 = controlPoints[Clamp(segment)];
        Transform p2 = controlPoints[Clamp(segment + 1)];
        Transform p3 = controlPoints[Clamp(segment + 2)];

        // Move the object
        transform.position = CatmullRom(p0.position, p1.position, p2.position, p3.position, localT);

        // Check for flipping when transitioning to a new segment
        if (segment != previousSegment)
        {
            // Determine facing direction based on flipAtControlPoints
            if (flipAtControlPoints != null && segment < flipAtControlPoints.Length)
            {
                spriteRenderer.flipX = flipAtControlPoints[segment]; // Flip based on the corresponding boolean
            }

            previousSegment = segment;
        }

        // Increment t by speed
        t += speed * Time.deltaTime;

        if (t >= controlPoints.Length - 2)
        {
            t = 0f; // Reset for looping
        }
    }

    int Clamp(int index)
    {
        return Mathf.Clamp(index, 0, controlPoints.Length - 1);
    }

    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * ((2f * p1) +
                       (-p0 + p2) * t +
                       (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                       (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
    }
}
