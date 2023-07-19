using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] public float maxVolume;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioClip menuMusic;
    [SerializeField] public AudioClip gameMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource.volume = maxVolume;
        audioSource.clip = menuMusic;
        
        audioSource.Play();
    }

    public AudioClip GetClip()
    {
        return audioSource.clip;
    }

    public void SetClip(AudioClip clip)
    {
        if (clip == audioSource.clip) return;
        
        audioSource.clip = clip;
        audioSource.Play();
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }

    public void SetVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }

    public float GetMaxVolume()
    {
        return maxVolume;
    }
}
