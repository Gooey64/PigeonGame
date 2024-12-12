using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript1 : MonoBehaviour
{
    public GameObject Pigeon; 
    public GameObject WallTop; 
    public GameObject WallBottom; 
    public GameObject WallLeft; 
    public GameObject WallRight; 
    public float heightOffset = 5f; 
    public float upViewOffset = 3f; 

    void Start()
    {
    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(Pigeon.transform.position.x, WallLeft.transform.position.x + 8.9f, WallRight.transform.position.x - 8.9f),
            Mathf.Clamp(Pigeon.transform.position.y + upViewOffset, WallBottom.transform.position.y + heightOffset, WallTop.transform.position.y - heightOffset),
            transform.position.z
        );
    }
}
