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
    [SerializeField] private bool startImmediateAlways;
    private void Start()
    {
        if (startImmediateAlways)
        {
            StartGame();
            return;
        }

        if (SceneHandler.Instance == null)
        {
            StartGame();
            return;
        }
        
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

        if (menuParent != null)
        {
            menuParent.SetActive(false);
        }
    }

    private void ShowMenu()
    {
        if (menuParent != null)
        {
            menuParent.SetActive(true);
        }
        
        startGameButton.onClick.AddListener(() =>
        {
            StartGame();
        });
        
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainMenuSceneIndex, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
        });
    }
}
