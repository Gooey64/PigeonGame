using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRideScript : MonoBehaviour
{
    public float distance;
    float min;
    float max;
    bool up;
    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.y;
        max = transform.position.y + distance;
        up = true;
    }

    // Update is called once per frame
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
            transform.position += Vector3.up * 0.03f;
        }
        else
        {
            transform.position += Vector3.down * 0.03f;
        }
    }
}
