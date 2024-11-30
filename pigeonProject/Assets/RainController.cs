using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    private ParticleSystem rain;

    void Start()
    {
        rain = GetComponent<ParticleSystem>();
    }

    public void StartRain()
    {
        if (!rain.isPlaying)
        {
            rain.Play();
        }
    }

    public void StopRain()
    {
        if (rain.isPlaying)
        {
            rain.Stop();
        }
    }
}
