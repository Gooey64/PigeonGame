using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField] private AudioClip catnoise;
    public float distance;
    float min;
    float max;
    bool right;
    bool sighted;

    Vector3 RightFace = new Vector3(2, 2, 2);
    Vector3 LeftFace = new Vector3(-2, 2, 2);
    bool catNoiseFlag = true;
    public float catNoiseInterval = 5f;
    public float moveSpeed = 2f; // Adjust as needed

    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x - distance;
        max = transform.position.x + distance;
        right = true;
        sighted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Stop movement if game is paused
        if (Time.timeScale == 0) return;

        if (!sighted)
        {
            if (transform.position.x <= min)
            {
                right = true;
                transform.localScale = RightFace;
            }
            else if (transform.position.x >= max)
            {
                right = false;
                transform.localScale = LeftFace;
            }

            // Adjust movement with Time.deltaTime
            if (right)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            // Adjust faster movement with Time.deltaTime
            if (right)
            {
                transform.position += Vector3.right * moveSpeed * 3f * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.left * moveSpeed * 3f * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sighted = true;
            if (catNoiseFlag)
            {
                StartCoroutine(catMakeNoise());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sighted = false;
        }
    }

    IEnumerator catMakeNoise()
    {
        SoundFXManager.instance.PlaySoundFXClip(catnoise);
        catNoiseFlag = false;
        yield return new WaitForSeconds(catNoiseInterval);
        catNoiseFlag = true;
    }
}
