using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinArea : MonoBehaviour
{
    private const string PLAYERPREFS_CURRENTSCORE = "CurrentScore";
    private const string PLAYERPREFS_LEVELNAME = "LevelName";
    private const string PLAYERPREFS_LEVELPATH = "LevelPath";

    private int openHash = Animator.StringToHash("Open");

    [SerializeField] private string levelName;
    [SerializeField] private string levelPath;
    [SerializeField] private float waitTime;
    [SerializeField] private Animator animator;
    [SerializeField] private Timer timer;
    [SerializeField] private AudioClip winClip;

    private bool hasWon = false;

    [SerializeField] private bool useWithLeaderboard;
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return;
        if (!other.CompareTag("Player")) return;

        if (animator != null)
        {
            animator.SetTrigger(openHash);
        }

        hasWon = true;

        timer.enabled = false;
        float gameTime = timer.GetGameTime();

        if (hasWon && useWithLeaderboard)
        {
            PlayerPrefs.SetFloat(PLAYERPREFS_CURRENTSCORE, gameTime);
            PlayerPrefs.SetString(PLAYERPREFS_LEVELNAME, levelName);
            PlayerPrefs.SetString(PLAYERPREFS_LEVELPATH, levelPath);
        }

        if (winClip != null)
        {
            AudioSource.PlayClipAtPoint(winClip, Camera.main.transform.position, 0.5f);
        }

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitTime);
        
        SceneHandler.Instance.LoadScene(SceneHandler.Instance.postGameScene, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
    }
}
