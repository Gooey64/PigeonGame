using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCollider : MonoBehaviour
{
    public bool isTrigger = false;

    private readonly KeyCode getPackageKey = KeyCode.E;
    private GameObject package;

    void Start()
    {
        this.package = transform.parent.gameObject;
    }

    void Update()
    {
        if (isTrigger)
        {
            if (Input.GetKeyDown(getPackageKey))
            {
                PickUpPackage();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("enter");
            this.isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.isTrigger = false;
        }
    }

    void PickUpPackage()
    {
        Debug.Log("package picked up!");
        Destroy(package);
    }
}
