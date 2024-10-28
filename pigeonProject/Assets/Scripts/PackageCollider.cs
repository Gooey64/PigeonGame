using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCollider : MonoBehaviour
{
    public bool isTrigger = false;

    private readonly KeyCode getPackageKey = KeyCode.E;
    private GameObject package;
    private GameObject packageHandler;

    void Start()
    {
        this.package = transform.parent.gameObject;
        this.packageHandler = GameObject.FindWithTag("PackageHandler");
        packageHandler.GetComponent<PackageHandler>().OnPackageDelivery += this.DeliverPackage;
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
            // Debug.Log("enter");
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
        packageHandler.GetComponent<PackageHandler>().PickUpPackage(gameObject);
        Destroy(package);
    }

    // destroy the package when delivered
    void DeliverPackage(GameObject _) {
        Destroy(package);
    }
}
