using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject levelSelectPanel;

    public void ShowMainMenu()
    {
        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(false); 
        }
    }
}