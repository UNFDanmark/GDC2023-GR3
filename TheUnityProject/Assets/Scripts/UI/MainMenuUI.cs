using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button levelOneButton;
    [SerializeField] private Button levelTwoButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button creditsButton;

    void Start()
    {
        levelOneButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.levelOneSceneIndex, MusicPlayer.Instance.gameMusic, SceneLoadType.Load);
        }); 
        
        levelTwoButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.levelTwoSceneIndex, MusicPlayer.Instance.gameMusic, SceneLoadType.Load);
        }); 
        
        tutorialButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.tutorialSceneIndex, MusicPlayer.Instance.gameMusic, SceneLoadType.Load);
        });

        leaderboardButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.leaderboardSceneIndex, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
        }); 
        
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        
        creditsButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.creditsSceneIndex, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
        });
    }
}
