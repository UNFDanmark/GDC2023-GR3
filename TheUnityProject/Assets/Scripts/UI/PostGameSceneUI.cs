using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PostGameSceneUI : MonoBehaviour
{
    private const string PLAYERPREFS_CURRENTSCORE = "CurrentScore";
    private const string path = "/HIGHSCORES.txt";

    [SerializeField] private bool testMode;
    [SerializeField] private TextMeshProUGUI playerTimeText;
    [SerializeField] private LeaderboardLineUI[] lines;
    
    private void Start()
    {
        HighscoreEntry[] entries;
        
        if (!File.Exists(Application.persistentDataPath + path))
        {
            //If no file - create file with empty entries and new player entry
            
            entries = HandleMissingFile();
        }
        else
        {
            //if file - read file
            
            StreamReader sr = new StreamReader(Application.persistentDataPath + path);
            string text = sr.ReadToEnd();
            sr.Close();
            
            HighscoreList highscoreList = JsonUtility.FromJson<HighscoreList>(text);
            entries = highscoreList.highscores;
        }

        if (testMode)
        {
            HandleTest(entries);
        }
        else
        {
            HandlePlayerWon(entries);
        }
    }

    private HighscoreEntry[] HandleMissingFile()
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + path);

        HighscoreList highscoreList = new HighscoreList();

        float currentTime = PlayerPrefs.GetFloat(PLAYERPREFS_CURRENTSCORE);
        PlayerPrefs.DeleteKey(PLAYERPREFS_CURRENTSCORE);
        PlayerPrefs.Save();
        
        highscoreList.highscores[0] = new HighscoreEntry(GetPlayerName(), 75f);
        
        for (int i = 1; i < highscoreList.highscores.Length; i++)
        {
            highscoreList.highscores[i] = new HighscoreEntry("Empty", -1f);
        }

        string json = JsonUtility.ToJson(highscoreList);

        sw.Write(json);
        sw.Close();

        return highscoreList.highscores;
    }

    private void HandlePlayerWon(HighscoreEntry[] entries)
    {
        //string playerName = GetPlayerName();
        
        float currentTime = PlayerPrefs.GetFloat(PLAYERPREFS_CURRENTSCORE);
        PlayerPrefs.DeleteKey(PLAYERPREFS_CURRENTSCORE);
        PlayerPrefs.Save();
        
        //Player's score's index on leaderboard - 0 is top
        //is -1 if not on leaderboard
        int playerEntryIndexInLeaderboard = -1;

        for (int i = 0; i < entries.Length; i++)
        {
            if (currentTime < entries[i].time)
            {
                playerEntryIndexInLeaderboard = i;
                break;
            }
        }

        for (int i = entries.Length - 1; i > playerEntryIndexInLeaderboard; i--)
        {
            entries[i] = entries[i - 1];
        }
        
        
    }

    private string GetPlayerName()
    {
        int stringlen = 4;
        int randValue;
        char letter;
        string name = "";
        
        for (int i = 0; i < stringlen; i++)
        {
  
            // Generating a random number.
            randValue = UnityEngine.Random.Range(0,26);
  
            // Generating random character by converting
            // the random number into character.
            letter = Convert.ToChar(randValue + 65);
  
            // Appending the letter to string.
            name = name + letter;
        }

        return name;
    }

    private void HandleTest(HighscoreEntry[] entries)
    {
        UpdateUI(entries);
    }

    private void UpdateUI(HighscoreEntry[] entries)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            HighscoreEntry entry = entries[i];

            string name = entry.name;
            float time = entry.time;

            string timeString = TimeConverter.TimeFloatToString(time);

            lines[i].timeText.text = timeString;
            lines[i].nameText.text = name;
        }
    }
}
