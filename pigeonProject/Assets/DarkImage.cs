using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBackgroundManager : MonoBehaviour
{
    public GameObject darkBackground; 
    public GameObject messagePanel; 

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
