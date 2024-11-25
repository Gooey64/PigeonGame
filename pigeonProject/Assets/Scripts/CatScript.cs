using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
   public float distance;
    float min;
    float max;
    bool right;

   Vector3 RightFace = new Vector3(2, 2, 2);
   Vector3 LeftFace = new Vector3(-2, 2, 2);


    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x - distance;
        max = transform.position.x + distance;
        right = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= min)
        {
            right = true;
            transform.localScale = RightFace;
        }
        else if (transform.position.x >= max)
        {
            right = false;
            transform.localScale = LeftFace;
        }

        if (right)
        {
            transform.position += Vector3.right * 0.02f;
        }
        else
        {
            transform.position += Vector3.left * 0.02f;
        }
    }



}

