using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class platformMove : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public Rigidbody2D platformRigidbody;
    public float moveSpeed = 1.0f;

    private int dir = 1;

    void FixedUpdate()
    {
        platformRigidbody.velocity = new Vector2(dir * moveSpeed, platformRigidbody.velocity.y);

        if (platformRigidbody.position.x >= right.position.x && dir == 1)
        {
            dir = -1;
        }
        else if (platformRigidbody.position.x <= left.position.x && dir == -1)
        {
            dir = 1;
        }
    }
}
