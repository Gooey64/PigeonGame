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
            if (ScoreManager.Instance.HasReachedTargetScore())
            {
                Debug.Log("Target score reached. Delivering package...");
                DeliverPackage(other.gameObject);
            }
            else
            {
                Debug.Log("Target score not reached. Delivery ignored.");
                IgnoreDelivery(other.gameObject);
            }
        }
    }

    void DeliverPackage(GameObject package)
    {
        if (packageHandler != null)
        {
            packageHandler.GetComponent<PackageHandler>().DeliverPackage(package);
            Debug.Log("Package delivered successfully.");
        }
        else
        {
            Debug.LogError("PackageHandler not found. Cannot deliver package.");
        }
    }

    void IgnoreDelivery(GameObject package)
    {
        Debug.Log("Package delivery ignored. Returning package to dynamic state.");
        Rigidbody2D rb = package.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; 
        }
    }
}
