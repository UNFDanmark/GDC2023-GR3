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
    public int gameSceneIndex;
    public int postGameScene;
    public int tutorialSceneIndex; 
    public int controlsSceneIndex;
    public int leaderboardSceneIndex;
        
    [Header("Fading")]
    [SerializeField] private float fadeOutTime = 1f; /*Amount of seconds it takes to fade to black.*/
    [SerializeField] private float fadeInTime = 2f; /*Amount of seconds it takes to fade back in.*/
    [SerializeField] private float startFadeWaitTime = 0.5f; /*Amount of seconds before the screen starts to fade out,*/
    [SerializeField] private float fadeWaitTime = 0.5f; /*The amount of seconds the screen remains black.*/
    
    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;

    public SceneLoadType SceneLoadType;
    private Coroutine currentActiveFade;
    private bool isLoading;
    
    private void Awake()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentActiveFade = null;
        isLoading = false;

        canvasGroup.alpha = 0;
    }

    public void LoadScene(int index, SceneLoadType _sceneLoadType)
    {
        if (isLoading) return;

        SceneLoadType = _sceneLoadType;
        
        StartCoroutine(Transition(index));
    }

    private IEnumerator Transition(int index)
    {
        isLoading = true;

        yield return new WaitForSeconds(startFadeWaitTime);

        yield return FadeOut(fadeOutTime);

        yield return SceneManager.LoadSceneAsync(index);

        yield return new WaitForSeconds(fadeWaitTime);

        FadeIn(fadeInTime);
        
        isLoading = false;
    }
    
    private Coroutine FadeOut(float time)
    {
        return Fade(1, 0, time);
    }

    private Coroutine FadeIn(float time)
    {
        return Fade(0, 1, time);
    }

    private Coroutine Fade(float alphaTarget, float audioTarget, float time)
    {
        if (currentActiveFade != null)
        {
            StopCoroutine(currentActiveFade);
        }

        currentActiveFade = StartCoroutine(FadeRoutine(alphaTarget, audioTarget, time));
        return currentActiveFade;
    }

    private IEnumerator FadeRoutine(float alphaTarget, float audioTarget, float time)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, alphaTarget))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, alphaTarget, Time.deltaTime / time);

            yield return null;
        }

        canvasGroup.alpha = alphaTarget;
    }
}
