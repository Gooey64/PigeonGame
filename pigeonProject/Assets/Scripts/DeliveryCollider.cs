using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCollider : MonoBehaviour
{
    private GameObject deliverySpot;
    private GameObject packageHandler;

    void Start()
    {
        this.deliverySpot = transform.parent.gameObject;
        this.packageHandler = GameObject.FindWithTag("PackageHandler");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package"))
        {
            DeliverPackage();
        }
    }

    void DeliverPackage()
    {
        packageHandler.GetComponent<PackageHandler>().DeliverPackage(gameObject);
    }
}
