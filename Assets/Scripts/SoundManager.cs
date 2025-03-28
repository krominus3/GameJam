using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // ��������

    public AudioSource musicSource; // �������� ������
    public AudioSource sfxSource;   // �������� �������� ��������

    public AudioClip backgroundMusic; // ������� ������

    private void Awake()
    {
        // ��������� Singleton
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

    // ��������������� ������
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // ��������������� ��������� �������
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // ��������� ���������
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
