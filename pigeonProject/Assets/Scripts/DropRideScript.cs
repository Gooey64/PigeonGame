using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRideScript : MonoBehaviour
{
    public float distance; 
    public float speed = 0.03f; 
    private float min;
    private float max;
    private bool up;

    void Start()
    {
        min = transform.position.y;
        max = transform.position.y + distance;
        up = true;
    }

    void Update()
    {
        if (transform.position.y <= min)
        {
            up = true;
        }
        else if (transform.position.y >= max)
        {
            up = false;
        }

        if (up)
        {
            transform.position += Vector3.up * speed;
        }
        else
        {
            transform.position += Vector3.down * speed;
        }
    }
}
