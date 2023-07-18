using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour, IStartable
{
    public static Timer Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI timerText;
    
    private float startTime;
    private float gameTime;

    private bool hasStarted;
    
    private void Awake()
    {
        Instance = this;

        hasStarted = false;
        timerText.text = "00:00";
    }
    
    public void StartGame()
    {
        hasStarted = true;
        startTime = Time.time;
    }

    private void Update()
    {
        if (!hasStarted) return;
        
        gameTime = Time.time - startTime;

        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        
        string minutesText = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        string secondsText = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        
        timerText.text = $"{minutesText}:{secondsText}";
    }

    public float GetGameTime()
    {
        return gameTime;
    }
}
