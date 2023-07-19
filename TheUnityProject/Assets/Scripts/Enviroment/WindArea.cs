using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour, IStartable
{

    [SerializeField] private float windForce;
    private PlayerStateMachine playerStateMachine;
    private bool Blow;
    private bool playerInWind;
    private float timer;
    [SerializeField] private float startDelayDuration;
    [SerializeField] private float windDurationShort;
    [SerializeField] private float windDurationLong;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float audioFadeTime;
    [SerializeField] private AudioSource audioSource;

    private Coroutine currentActiveFade;
    
    public void StartGame()
    {
        playerStateMachine = PlayerStateMachine.Instance;
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(startDelayDuration);
        
        while (true)
        {
            Blow = true;
            audioSource.Play();
            StartFade(1);
            yield return new WaitForSeconds(windDurationShort);
            particleSystem.Play();
            yield return new WaitForSeconds(windDurationLong);
            particleSystem.Stop();
            StartFade(0);
            audioSource.Stop();
            yield return new WaitForSeconds(windDurationShort);
            Blow = false;
            yield return new WaitForSeconds(windDurationShort);
        }
    }

    
    private void StartFade(float targetAudio)
    {
        if (currentActiveFade != null)
        {
            StopCoroutine(currentActiveFade);
        }

        currentActiveFade = StartCoroutine(Fade(targetAudio, audioFadeTime));
    }

    private IEnumerator Fade(float targetVolume, float time)
    {
        while (!Mathf.Approximately(audioSource.volume, targetVolume))
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume,
                Time.deltaTime / time);
            yield return null;
        }

    }

    private void FixedUpdate()
    {
        if (Blow && playerInWind)
        {
            playerStateMachine.AddVerticalVelocity(windForce * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWind = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInWind = false;
        }
    }
}
