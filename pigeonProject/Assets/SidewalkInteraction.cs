using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewalkInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pigeon")) // Ensure the pigeon has the "Pigeon" tag
        {
            Animator pigeonAnimator = other.GetComponent<Animator>();
            if (pigeonAnimator != null)
            {
                Debug.Log("Pigeon landed on the sidewalk.");
                pigeonAnimator.enabled = false; // Turn off the animator
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pigeon")) // Ensure the pigeon has the "Pigeon" tag
        {
            Animator pigeonAnimator = other.GetComponent<Animator>();
            if (pigeonAnimator != null)
            {
                Debug.Log("Pigeon left the sidewalk.");
                pigeonAnimator.enabled = true; // Turn on the animator
            }
        }
    }
}
