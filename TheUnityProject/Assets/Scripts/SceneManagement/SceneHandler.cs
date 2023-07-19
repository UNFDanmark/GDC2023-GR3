using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }

    [Header("Scene indexes")]
    public int mainMenuSceneIndex;
    public int levelOneSceneIndex;
    public int levelTwoSceneIndex;
    public int postGameScene;
    public int tutorialSceneIndex;
    public int leaderboardSceneIndex;
        
    [Header("Fading")]
    [SerializeField] private float fadeOutTime = 1f; /*Amount of seconds it takes to fade to black.*/
    [SerializeField] private float fadeInTime = 2f; /*Amount of seconds it takes to fade back in.*/
    [SerializeField] private float startFadeWaitTime = 0.5f; /*Amount of seconds before the screen starts to fade out,*/
    [SerializeField] private float fadeWaitTime = 0.5f; /*The amount of seconds the screen remains black.*/
    
    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fadeOutClip;
    [SerializeField] private AudioClip fadeInClip;
    
    public SceneLoadType SceneLoadType;
    private Coroutine currentActiveFade;
    private bool isLoading;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);   
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentActiveFade = null;
        isLoading = false;

        canvasGroup.alpha = 0;
    }

    public void LoadScene(int index, AudioClip clip, SceneLoadType _sceneLoadType)
    {
        if (isLoading) return;

        SceneLoadType = _sceneLoadType;
        
        StartCoroutine(Transition(index, clip));
    }

    private IEnumerator Transition(int index, AudioClip clip)
    {
        isLoading = true;
        bool shouldChangeMusic = MusicPlayer.Instance.GetClip() != clip;

        yield return new WaitForSeconds(startFadeWaitTime);

        /*
        audioSource.clip = fadeOutClip;
        audioSource.Play();
        */
        yield return FadeOut(fadeOutTime, shouldChangeMusic);

        yield return SceneManager.LoadSceneAsync(index);

        yield return new WaitForSeconds(fadeWaitTime);

        if (shouldChangeMusic)
        {
            MusicPlayer.Instance.SetClip(clip);
        }
        
        /*
        audioSource.clip = fadeInClip;
        audioSource.Play();
        */
        
        FadeIn(fadeInTime, shouldChangeMusic);
        
        isLoading = false;
    }
    
    private Coroutine FadeOut(float time, bool shouldChangeMusic)
    {
        return Fade(MusicPlayer.Instance.GetVolume(), 0, time, shouldChangeMusic);
    }

    private Coroutine FadeIn(float time, bool shouldChangeMusic)
    {
        return Fade(MusicPlayer.Instance.GetVolume(), MusicPlayer.Instance.GetMaxVolume(), time, shouldChangeMusic);
    }

    private Coroutine Fade(float alphaTarget, float audioTarget, float time, bool shouldChangeMusic)
    {
        if (currentActiveFade != null)
        {
            StopCoroutine(currentActiveFade);
        }

        currentActiveFade = StartCoroutine(FadeRoutine(alphaTarget, audioTarget, time, shouldChangeMusic));
        return currentActiveFade;
    }

    private IEnumerator FadeRoutine(float alphaTarget, float audioTarget, float time, bool shouldChangeMusic)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, alphaTarget))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, alphaTarget, Time.deltaTime / time);

            if (shouldChangeMusic)
            {
                MusicPlayer.Instance.SetVolume(Mathf.MoveTowards(MusicPlayer.Instance.GetVolume(), audioTarget,
                    Time.deltaTime / time));
            }

            yield return null;
        }

        canvasGroup.alpha = alphaTarget;
    }
}
