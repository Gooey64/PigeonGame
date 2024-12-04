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

            if (right)
            {
                transform.position += Vector3.right * 0.02f;
            }
            else
            {
                transform.position += Vector3.left * 0.02f;
            }
        }
        else
        {
            if (right)
            {
                transform.position += Vector3.right * 0.06f;
            }
            else
            {
                transform.position += Vector3.left * 0.06f;
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

