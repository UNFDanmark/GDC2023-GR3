using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeConverter
{
    public static string TimeFloatToString(float _seconds)
    {
        if (_seconds == -1)
        {
            return "00:00:00";
        }
        
        int minutes = Mathf.FloorToInt(_seconds / 60);
        int seconds = Mathf.FloorToInt(_seconds - minutes * 60);
        
        int milliseconds = Mathf.FloorToInt(_seconds * 100 - minutes * 60 * 100 - seconds * 100);
        
        //string minutesText = minutes < 10 ? "0" + minutes.ToString() : minutes.ToString();
        string minutesText;
        string secondsText;
        string millisecondsText;

        if (minutes < 10)
        {
            minutesText = "0" + minutes.ToString();
        }
        else
        {
            minutesText = minutes.ToString();
        }
        
        //string secondsText = seconds < 10 ? "0" + seconds.ToString() : seconds.ToString();
        if (seconds < 10)
        {
            secondsText = "0"+ seconds.ToString();
        }
        else
        {
            secondsText = seconds.ToString();
        }
        
        //string millisecondsText = milliseconds < 10 ? "0" + milliseconds.ToString() : milliseconds.ToString();
        if (milliseconds < 10)
        {
            millisecondsText = "0"+ milliseconds.ToString();
        }
        else
        {
            millisecondsText= milliseconds.ToString();
        }
        
        return $"{minutesText}:{secondsText}:{millisecondsText}";
    }
}
