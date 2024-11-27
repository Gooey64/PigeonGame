using System.Collections;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip)
    {
        Debug.Log(audioClip);
        AudioSource currentAudioSource = Instantiate(soundFXObject, this.transform);
        currentAudioSource.clip = audioClip;
        currentAudioSource.volume = 1f;
        currentAudioSource.Play();

        Destroy(currentAudioSource.gameObject, currentAudioSource.clip.length);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip)
    {
        int rand = Random.Range(0, audioClip.Length);

        AudioSource currentAudioSource = Instantiate(soundFXObject, this.transform);
        currentAudioSource.clip = audioClip[rand];
        currentAudioSource.volume = 1f;
        currentAudioSource.Play();

        Destroy(currentAudioSource.gameObject, currentAudioSource.clip.length);
    }

    public AudioSource StartLoopingSoundFXClip(AudioClip audioClip)
    {
        AudioSource currentAudioSource = Instantiate(soundFXObject, this.transform);
        currentAudioSource.loop = true;
        currentAudioSource.clip = audioClip;
        currentAudioSource.volume = 1f;
        currentAudioSource.Play();

        return currentAudioSource;
    }
}
