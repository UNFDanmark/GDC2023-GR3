using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinArea : MonoBehaviour
{
    private const string PLAYERPREFS_CURRENTSCORE = "CurrentScore";
    
    [SerializeField] private Timer timer;

    private bool hasWon = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;
        if (!other.CompareTag("Player")) return;
        
        hasWon = true;

        timer.enabled = false;
        float gameTime = timer.GetGameTime();

        if (hasWon)
        {
            PlayerPrefs.SetFloat(PLAYERPREFS_CURRENTSCORE, gameTime);
        }

        SceneHandler.Instance.LoadScene(SceneHandler.Instance.postGameScene, SceneLoadType.Load);
    }
}
