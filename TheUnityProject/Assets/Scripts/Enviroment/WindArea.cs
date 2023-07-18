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

    public void StartGame()
    {
        playerStateMachine = PlayerStateMachine.Instance;
        StartCoroutine(Run());
    }
    
    private void Update()
    {
        /*
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = windDuration;

            switch (Blow)
            {
                case true:
                    particleSystem.Stop();
                    Blow = false;
                    break;
                case false:
                    particleSystem.Play();
                    Blow = true;
                    break;
            }
        }
        */
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(startDelayDuration);
        
        while (true)
        {
            Blow = true;
            yield return new WaitForSeconds(windDurationShort);
            particleSystem.Play();
            yield return new WaitForSeconds(windDurationLong);
            particleSystem.Stop();
            yield return new WaitForSeconds(windDurationShort);
            Blow = false;
            yield return new WaitForSeconds(windDurationShort);
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
