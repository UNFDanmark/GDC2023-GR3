using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    public event EventHandler OnPress;
    
    [SerializeField] private Button returnButton;
    void Start()
    {
        returnButton.onClick.AddListener(() =>
        {
            OnPress?.Invoke(this, EventArgs.Empty);
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainMenuSceneIndex, SceneLoadType.Load);
        });
    }

}
