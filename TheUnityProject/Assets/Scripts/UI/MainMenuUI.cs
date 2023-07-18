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
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.gameSceneIndex, SceneLoadType.Load);
        });  
        
        controlsButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.controlsSceneIndex, SceneLoadType.Load);
        }); 
        
        leaderboardButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.leaderboardSceneIndex, SceneLoadType.Load);
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
