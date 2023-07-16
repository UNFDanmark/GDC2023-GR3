using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{

    [SerializeField] private float windForce;
    [SerializeField] private PlayerStateMachine PlayerState;
    private bool Blow;
    
    private void FixedUpdate()
    {
        if (Blow)
        {
            PlayerState.AddVerticalVelocity(windForce * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blow = false;
        }
    }
}
