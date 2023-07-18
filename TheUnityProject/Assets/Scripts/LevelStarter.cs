using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelStarter : MonoBehaviour
{
    [SerializeField] private GameObject menuParent;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        if (SceneHandler.Instance.SceneLoadType == SceneLoadType.Load)
        {
            StartGame();
        }
        else
        {
            ShowMenu();
        }
    }

    private void StartGame()
    {
        foreach (IStartable startable in FindObjectsOfType<MonoBehaviour>().OfType<IStartable>())
        {
            startable.StartGame();
        }
        
        PlayerStateMachine.Instance.StartPlayer();
        menuParent.SetActive(false);
    }

    private void ShowMenu()
    {
        menuParent.SetActive(true);
        
        startGameButton.onClick.AddListener(() =>
        {
            StartGame();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainMenuSceneIndex, SceneLoadType.Load);
        });
    }
}
