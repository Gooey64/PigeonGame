using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDisappear : MonoBehaviour
{
    public GameObject targetImage; 
    private static bool imageAlreadyHidden = false;

    void Start()
    {
        if (imageAlreadyHidden)
        {
            HideImage();
        }
    }

    void Update()
    {
        if (HealthManager.PlayerDied || PauseMenu.IsRestartClicked())
        {
            HideImage();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || 
                 Input.GetButtonDown("Action") || 
                 Input.GetKeyDown(KeyCode.D))
        {
            HideImage();
        }
    }

    void HideImage()
    {
        if (targetImage != null && targetImage.activeSelf)
        {
            targetImage.SetActive(false); 
            imageAlreadyHidden = true; 
            Debug.Log("Image has been hidden.");
        }
        else
        {
            Debug.Log("No image assigned or image is already hidden.");
        }
    }
}
