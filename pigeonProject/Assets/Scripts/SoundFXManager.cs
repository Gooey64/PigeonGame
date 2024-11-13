using System.Collections;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    private AudioSource currentAudioSource;

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

    public void PlaySoundFXClip(AudioClip audioClip, Transform spanTransform, float volume)
    {
        if (currentAudioSource != null)
        {
            Destroy(currentAudioSource.gameObject);
        }

        currentAudioSource = Instantiate(soundFXObject, spanTransform.position, Quaternion.identity);
        currentAudioSource.clip = audioClip;
        currentAudioSource.volume = volume;
        currentAudioSource.Play();

        Destroy(currentAudioSource.gameObject, currentAudioSource.clip.length);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spanTransform, float volume)
    {
        int rand = Random.Range(0, audioClip.Length);

        if (currentAudioSource != null)
        {
            Destroy(currentAudioSource.gameObject);
        }

        currentAudioSource = Instantiate(soundFXObject, spanTransform.position, Quaternion.identity);
        currentAudioSource.clip = audioClip[rand];
        currentAudioSource.volume = volume;
        currentAudioSource.Play();

        Destroy(currentAudioSource.gameObject, currentAudioSource.clip.length);
    }

    public bool IsPlaying()
    {
        return currentAudioSource != null && currentAudioSource.isPlaying;
    }

    public void StopSoundFX()
    {
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        if (currentAudioSource != null)
        {
            currentAudioSource.volume = volume;
        }
    }

}
