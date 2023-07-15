using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour
{
    [SerializeField] private Timer timer;

    private bool hasWon = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;

        if (!other.CompareTag("Player")) return;
        hasWon = true;

        timer.enabled = false;
        float gameTime = timer.GetGameTime();
        Debug.Log("Win!");
    }
}
