using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinArea : MonoBehaviour
{
    private const string PLAYERPREFS_HIGHSCORE_KEY = "Highscore";
    
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

        float highscore = Mathf.Infinity;

        if (PlayerPrefs.HasKey(PLAYERPREFS_HIGHSCORE_KEY))
        {
            highscore = PlayerPrefs.GetFloat(PLAYERPREFS_HIGHSCORE_KEY);
        }

        if (gameTime < highscore)
        {
            PlayerPrefs.SetFloat("Highscore", gameTime);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene(3);
    }
}
