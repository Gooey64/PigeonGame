using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    // Start is called before the first frame update

    public void Play()
    {
        SoundFXManager.instance.PlaySoundFXClip(clip);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
