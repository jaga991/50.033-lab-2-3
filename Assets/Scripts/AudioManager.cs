using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance

    public AudioSource musicSource;  // For background music
    public AudioSource sfxSource;    // For sound effects

    
    public AudioClip backgroundMusic;
    public AudioClip defaultSFX;

    void Awake()
    {
        // Singleton pattern: Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
        sfxSource.time = 0.02f;
    }

    // Play sound effects
    public void PlaySFX(AudioClip clip = null)
    {
        if (clip == null)
        {
            clip = defaultSFX;
        }

        if (clip != null) {

            sfxSource.PlayOneShot(clip);
        }
   
    }

    // Play background music
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Adjust volume
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
