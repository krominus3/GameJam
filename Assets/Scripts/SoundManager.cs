using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Синглтон

    public AudioSource musicSource; // Источник музыки
    public AudioSource sfxSource;   // Источник звуковых эффектов

    public AudioClip backgroundMusic; // Фоновая музыка

    private void Awake()
    {
        // Реализуем Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    // Воспроизведение музыки
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // Воспроизведение звукового эффекта
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Установка громкости
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
