using UnityEngine;
using UnityEngine.UI;

public class FlyControlTutorial : MonoBehaviour
{
    public GameObject highlightArrow; 
    public Text instructionText;    
    private bool tutorialActive = true;

    void Start()
    {
        // Time.timeScale = 0;
        // ShowHighlight();
    }

    void Update()
    {
        // if (tutorialActive && Input.GetKeyDown(KeyCode.UpArrow)) 
        // {
        //     EndTutorial();
        // }
    }

    void ShowHighlight()
    {
        // highlightArrow.SetActive(true);
        // instructionText.text = "Press [Up] to flap your wings!";
        // instructionText.gameObject.SetActive(true);
    }

    void EndTutorial()
    {
        // highlightArrow.SetActive(false);
        // instructionText.gameObject.SetActive(false);
        // tutorialActive = false;
        // Time.timeScale = 1; 
    }
}
