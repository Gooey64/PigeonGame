using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject Pigeon;
    public GameObject WallTop;
    public GameObject WallBottom;
    public GameObject WallLeft;
    public GameObject WallRight;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
         Mathf.Clamp(Pigeon.transform.position.x, WallLeft.transform.position.x + 8.9f, WallRight.transform.position.x - 8.9f),
         Mathf.Clamp(Pigeon.transform.position.y, WallBottom.transform.position.y + 5f, WallTop.transform.position.y - 5f),
         transform.position.z);
    }
}
