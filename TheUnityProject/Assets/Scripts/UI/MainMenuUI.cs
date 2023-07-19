using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MainMenuUI : MonoBehaviour
{
    private const string path = "/HIGHSCORES.txt";
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button deleteHighscoresButton;
    
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.gameSceneIndex, MusicPlayer.Instance.gameMusic, SceneLoadType.Load);
        });  
        
        tutorialButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.tutorialSceneIndex, MusicPlayer.Instance.gameMusic, SceneLoadType.Load);
        });  
        
        controlsButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.controlsSceneIndex, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
        }); 
        
        leaderboardButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.leaderboardSceneIndex, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
        }); 
        
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        }); 
        deleteHighscoresButton.onClick.AddListener(() =>
        {
            File.Delete(Application.persistentDataPath + path);
        });
    }
}
