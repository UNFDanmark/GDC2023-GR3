using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeConverter
{
    public static string TimeFloatToString(float _seconds)
    {
        if (_seconds == -1)
        {
            return "00:00";
        }
        
        int minutes = Mathf.FloorToInt(_seconds / 60);
        int seconds = Mathf.FloorToInt(_seconds - minutes * 60);
        
        string minutesText = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        string secondsText = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();

        return $"{minutesText}:{secondsText}";
    }
}
