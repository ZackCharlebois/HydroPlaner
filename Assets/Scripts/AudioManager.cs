using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip musicClip;

    [Range(0f,1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeAudioManager();
    }

    private void InitializeAudioManager()
    {
        if (musicSource)
        {
            musicSource.volume = musicVolume;
        }
        if (sfxSource)
        {
            sfxSource.volume = sfxVolume;
        }
        Debug.Log("Audio Initialized");
    }

    public void PlayMusic(AudioSource music)
    {
        if (musicSource == null) return;
        music.Play();
    }

    public void PlayMusicClip(AudioClip clip)
    {
        if (!musicSource || clip == null)
        {
            return;
        }
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.Play();
        Debug.Log("Now playing music");
    }

    public void StopMusic() 
    {
        if (musicSource) musicSource.Stop();    
    }

    public void PlaySFX(AudioSource source)
    {
        if (!source || source.clip == null) return;

        source.PlayOneShot(source.clip, sfxVolume);
    }

    public void PlaySFXClip(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void StopSFX(AudioSource source)
    {
        if (source == null) return;
        source.Stop();
        Debug.Log("SFX Stopped");
    }





}
