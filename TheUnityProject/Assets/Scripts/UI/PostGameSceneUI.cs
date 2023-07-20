using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGameSceneUI : MonoBehaviour
{
    private const string PLAYERPREFS_CURRENTSCORE = "CurrentScore";
    private const string PLAYERPREFS_LEVELNAME = "LevelName";
    private const string PLAYERPREFS_LEVELPATH = "LevelPath";

    [SerializeField] private bool testMode;
    [SerializeField] private LeaderboardLineUI[] lines;
    [SerializeField] private ReturnButton returnButton;
    
    private void Start()
    {
        HighscoreList highscoreList;

        string sceneName = PlayerPrefs.GetString(PLAYERPREFS_LEVELNAME);
        string scenePath = PlayerPrefs.GetString(PLAYERPREFS_LEVELPATH);
        
        string path = Application.persistentDataPath + "/" + scenePath + ".txt";
        
        if (!File.Exists(path))
        {
            //If no file - create file with empty entries and new player entry
            
            highscoreList = HandleMissingFile();
        }
        else
        {
            //if file - read file
            
            StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            sr.Close();
            
            highscoreList = JsonUtility.FromJson<HighscoreList>(text);
        }

        if (testMode)
        {
            HandleTest(highscoreList);
        }
        else
        {
            HandlePlayerWon(highscoreList);
        }
    }

    private HighscoreList HandleMissingFile()
    {
        string sceneName = PlayerPrefs.GetString(PLAYERPREFS_LEVELNAME);
        string scenePath = PlayerPrefs.GetString(PLAYERPREFS_LEVELPATH);
        
        string path = Application.persistentDataPath + "/" + scenePath + ".txt";
        
        StreamWriter sw = new StreamWriter(path);

        HighscoreList highscoreList = new HighscoreList();

        highscoreList.levelName = PlayerPrefs.GetString(PLAYERPREFS_LEVELNAME);
        
        PlayerPrefs.Save();

        for (int i = 0; i < highscoreList.highscores.Length; i++)
        {
            highscoreList.highscores[i] = new HighscoreEntry("Empty", -1f);
        }

        string json = JsonUtility.ToJson(highscoreList);

        sw.Write(json);
        sw.Close();

        return highscoreList;
    }

    private void HandlePlayerWon(HighscoreList highscoreList)
    {
        if (!PlayerPrefs.HasKey(PLAYERPREFS_CURRENTSCORE) || !PlayerPrefs.HasKey(PLAYERPREFS_LEVELNAME) || !PlayerPrefs.HasKey(PLAYERPREFS_LEVELPATH))
        {
            Debug.LogError("No player time found!");
            return;
        }
        
        float currentTime = PlayerPrefs.GetFloat(PLAYERPREFS_CURRENTSCORE);
        PlayerPrefs.DeleteKey(PLAYERPREFS_CURRENTSCORE);
        PlayerPrefs.Save();

        HighscoreEntry[] entries = highscoreList.highscores;
        
        //Player's score's index on leaderboard - 0 is top
        //is -1 if not on leaderboard
        int playerEntryIndexInLeaderboard = -1;

        for (int i = 0; i < entries.Length; i++)
        {
            if (currentTime < entries[i].time || entries[i].time < 0)
            {
                playerEntryIndexInLeaderboard = i;
                break;
            }
        }

        if (playerEntryIndexInLeaderboard < 0)
        {
            Debug.Log("Player did not beat any highscore");
            UpdateUI(entries);
            return;
        }
        
        for (int i = entries.Length - 1; i > playerEntryIndexInLeaderboard; i--)
        {
            if (i <= 0) break;
            
            entries[i] = entries[i - 1];
        }
        
        UpdateUI(entries, playerEntryIndexInLeaderboard, currentTime);

        lines[playerEntryIndexInLeaderboard].nameText.gameObject.SetActive(false);
        lines[playerEntryIndexInLeaderboard].nameInput.gameObject.SetActive(true);
        lines[playerEntryIndexInLeaderboard].nameInput.Select();
        
        SetEvents(highscoreList, playerEntryIndexInLeaderboard, currentTime);
    }

    private void SetEvents(HighscoreList highscoreList, int playerEntryIndexInLeaderboard, float currentTime)
    {
        //Keep name-input selected
        lines[playerEntryIndexInLeaderboard].nameInput.onDeselect.AddListener((string message) =>
        {
            lines[playerEntryIndexInLeaderboard].nameInput.Select();
        });

        //Save and change scene on submit
        lines[playerEntryIndexInLeaderboard].nameInput.onSubmit.AddListener((string message) =>
        {
            SubmitName(highscoreList, playerEntryIndexInLeaderboard, message, currentTime);
            SceneManager.LoadScene(0);
        });
        
        //Save on pressing return button
        returnButton.OnPress += (object sender, EventArgs e) =>
        {
            string playerName = lines[playerEntryIndexInLeaderboard].nameInput.text;
            SubmitName(highscoreList, playerEntryIndexInLeaderboard, playerName, currentTime);
        };
    }

    private void SubmitName(HighscoreList highscoreList, int playerIndexInLeaderboard, string _name, float _time)
    {
        HighscoreEntry[] entries = highscoreList.highscores;
        
        entries[playerIndexInLeaderboard] = new HighscoreEntry(_name, _time);
            
        string sceneName = PlayerPrefs.GetString(PLAYERPREFS_LEVELNAME);
        string scenePath = PlayerPrefs.GetString(PLAYERPREFS_LEVELPATH);
        
        string path = Application.persistentDataPath + "/" + scenePath + ".txt";
        
        StreamWriter sw = new StreamWriter(path);

        string json = JsonUtility.ToJson(highscoreList);
        
        sw.Write(json);
        
        sw.Close();
    }

    private void HandleTest(HighscoreList highscoreList)
    {
        UpdateUI(highscoreList.highscores);
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
    
    private void UpdateUI(HighscoreEntry[] entries, int ignoreIndex, float ignoreIndexTime)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == ignoreIndex)
            {
                lines[i].nameText.gameObject.SetActive(false);
                lines[i].timeText.text = TimeConverter.TimeFloatToString(ignoreIndexTime);
                
                continue;
            }
            
            HighscoreEntry entry = entries[i];

            string name = entry.name;
            float time = entry.time;

            string timeString = TimeConverter.TimeFloatToString(time);

            lines[i].timeText.text = timeString;
            lines[i].nameText.text = name;
        }
    }
}
