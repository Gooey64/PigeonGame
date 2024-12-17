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
                DeliverPackage();
            }
            else
            {
                Debug.Log("Target score not reached. Delivery ignored.");
                IgnoreDelivery(other.gameObject);
            }
        }
    }

    void DeliverPackage()
    {
        packageHandler.GetComponent<PackageHandler>().DeliverPackage(gameObject);
    }

    void IgnoreDelivery(GameObject package)
    {
        Rigidbody2D rb = package.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
