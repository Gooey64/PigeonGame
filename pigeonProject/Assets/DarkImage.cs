using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBackgroundManager : MonoBehaviour
{
    public GameObject darkBackground; // Dark background panel
    public GameObject messagePanel; // Message panel with the image

    public void ShowMessage()
    {
        darkBackground.SetActive(true);
        messagePanel.SetActive(true);
    }

    public void HideMessage()
    {
        darkBackground.SetActive(false);
        messagePanel.SetActive(false);
    }
}
