using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRideScript : MonoBehaviour
{
    public float distance; 
    public float duration = 2f; 
    private float min;
    private float max;
    private float timer; 
    private bool up;

    void Start()
    {
        min = transform.position.y;
        max = transform.position.y + distance;
        timer = 0f;
        up = true;
    }

    void Update()
    {
        timer += Time.deltaTime;


        if (transform.position.y <= min && !up)
        {
            up = true;
            timer = 0f;
        }
        else if (transform.position.y >= max && up)
        {
            up = false;
            timer = 0f;
        }

        float step = (max - min) / duration * Time.deltaTime; 
        transform.position += (up ? Vector3.up : Vector3.down) * step;
    }
}
