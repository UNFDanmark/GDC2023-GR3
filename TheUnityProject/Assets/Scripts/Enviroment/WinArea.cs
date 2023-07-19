using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinArea : MonoBehaviour
{
    private const string PLAYERPREFS_CURRENTSCORE = "CurrentScore";

    private int openHash = Animator.StringToHash("Open");
    
    [SerializeField] private float waitTime;
    [SerializeField] private Animator animator;
    [SerializeField] private Timer timer;

    private bool hasWon = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;
        if (!other.CompareTag("Player")) return;

        animator.SetTrigger(openHash);
        
        hasWon = true;

        timer.enabled = false;
        float gameTime = timer.GetGameTime();

        if (hasWon)
        {
            PlayerPrefs.SetFloat(PLAYERPREFS_CURRENTSCORE, gameTime);
        }

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitTime);
        
        SceneHandler.Instance.LoadScene(SceneHandler.Instance.postGameScene, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
    }
}
