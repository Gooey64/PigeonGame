using UnityEngine;
using System.Collections;

public class UIElementShaker : MonoBehaviour
{
    public RectTransform textRectTransform;
    public GameObject staminaBar;
    public float shakeDuration = 1f;
    public float shakeAmount = 10f;
    public float shakeSpeed = 50f;

    private Vector3 originalTextPosition;
    private Vector3 originalStaminaPosition;

    void Start()
    {
        if (textRectTransform != null)
        {
            originalTextPosition = textRectTransform.localPosition;
        }
        if (staminaBar != null)
        {
            originalStaminaPosition = staminaBar.transform.localPosition;
        }

        // Check if the level is being replayed
        if (PlayerPrefs.GetInt("LevelReplayed", 0) == 0)
        {
            StartCoroutine(ShakeElements());
        }
        else
        {
            Debug.Log("Level is being replayed. Skipping shake effect.");
        }
    }

    IEnumerator ShakeElements()
    {
        if (textRectTransform != null)
        {
            yield return ShakeUIElement(textRectTransform);
        }

        if (staminaBar != null)
        {
            yield return ShakeGameObject(staminaBar);
        }
    }

    IEnumerator ShakeUIElement(RectTransform rectTransform)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0
            );

            rectTransform.localPosition = originalTextPosition + randomOffset;

            elapsedTime += Time.deltaTime * shakeSpeed;
            yield return null;
        }

        rectTransform.localPosition = originalTextPosition;
    }

    IEnumerator ShakeGameObject(GameObject gameObject)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-shakeAmount, shakeAmount),
                Random.Range(-shakeAmount, shakeAmount),
                0
            );

            gameObject.transform.localPosition = originalStaminaPosition + randomOffset;

            elapsedTime += Time.deltaTime * shakeSpeed;
            yield return null;
        }

        gameObject.transform.localPosition = originalStaminaPosition;
    }
}
