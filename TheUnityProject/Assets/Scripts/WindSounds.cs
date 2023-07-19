using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WindSounds : MonoBehaviour
{
    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float xScale;
    [SerializeField, Range(0f,1f)] private float maxVolume;
    
    private void Update()
    {
        audioSource.volume = Mathf.PerlinNoise1D(Time.time * xScale) * maxVolume;
    }
}
