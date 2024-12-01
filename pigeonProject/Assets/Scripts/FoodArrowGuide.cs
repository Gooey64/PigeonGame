using UnityEngine;

public class FoodArrowGuide : MonoBehaviour
{
    public Transform pigeon; // Reference to the pigeon object
    public Transform arrow; // Reference to the arrow object
    private GameObject closestFood; // Stores the closest food item

    void Update()
    {
        // Find the closest food item
        closestFood = FindClosestFood();

       if (closestFood != null)
{
    Vector3 direction = closestFood.transform.position - pigeon.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    arrow.rotation = Quaternion.Euler(0, 0, angle);

    Debug.Log("Arrow Position: " + arrow.position);
    Debug.Log("Arrow Rotation: " + arrow.rotation.eulerAngles);
}

        else
        {
            Debug.Log("No food items found!");
        }
    }

    GameObject FindClosestFood()
    {
        GameObject[] foodItems = GameObject.FindGameObjectsWithTag("Food");
        Debug.Log("Number of food items found: " + foodItems.Length); // Debug number of items

        if (foodItems.Length == 0)
        {
            Debug.Log("No food items found!");
            return null;
        }

        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject food in foodItems)
        {
            float distance = Vector3.Distance(pigeon.position, food.transform.position);
            Debug.Log("Checking food item: " + food.name + ", Distance: " + distance); // Debug each item

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = food;
            }
        }

        if (closest == null)
        {
            Debug.Log("No closest food item identified.");
        }
        else
        {
            Debug.Log("Closest food item: " + closest.name + ", Distance: " + minDistance);
        }

        return closest;
    }

}
