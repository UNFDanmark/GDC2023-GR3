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
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button deleteHighscoresButton;
    
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(1);
        });  
        
        controlsButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(2);
        }); 
        
        leaderboardButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(3);
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
