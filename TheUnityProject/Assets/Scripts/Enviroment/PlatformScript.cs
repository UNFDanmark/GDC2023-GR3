using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    
    private const string PLAYER_TAG = "Player";
    private Vector3 standardPosition;
    [SerializeField] private Vector3 relativePosition;
    private bool IsUp = true;
    [SerializeField] float constT;


    private void Start()
    {
        standardPosition = transform.position; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            IsUp = false;
            other.transform.parent = transform;
        }
    }
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition;
        float T = Time.fixedDeltaTime * constT;
        Vector3 actualPosition;

        switch (IsUp)
        {
            case true:
                // Hvis spiller ikke er på platfromem
                desiredPosition = standardPosition;
                break;
            case false:
                // Hvis spiller er på platformen
                desiredPosition = standardPosition + relativePosition;
                break;
        }

        actualPosition = Vector3.Lerp(transform.position, desiredPosition, T);
        transform.position = actualPosition;
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag((PLAYER_TAG)))
        {
            IsUp = true;
            
            other.transform.parent = null;
        }
    }
}
