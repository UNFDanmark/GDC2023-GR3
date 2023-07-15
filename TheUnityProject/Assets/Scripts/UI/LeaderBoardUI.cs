using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    private const string PLAYERPREFS_HIGHSCORE_KEY = "Highscore";
    
    [SerializeField] private LeaderBoardHighscore[] lines;
    
    private void Start()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].gameObject.SetActive(false);
        }
    }

    private string TimeFloatToString(float _seconds)
    {
        int minutes = Mathf.FloorToInt(_seconds / 60);
        int seconds = Mathf.FloorToInt(_seconds - minutes * 60);
        
        string minutesText = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        string secondsText = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

        return $"{minutesText}:{secondsText}";
    }
}
