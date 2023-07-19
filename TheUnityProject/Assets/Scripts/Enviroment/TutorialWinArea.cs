using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWinArea : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    private int openHash = Animator.StringToHash("Open");
    
    [SerializeField] private float waitTime;
    [SerializeField] private Animator animator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainMenuSceneIndex, MusicPlayer.Instance.gameMusic, SceneLoadType.Load);
            
            if (animator != null)
            {
                animator.SetTrigger(openHash);
            }

            StartCoroutine(LoadScene());
        }
    }
    
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitTime);
        
        SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainMenuSceneIndex, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
    }
}
