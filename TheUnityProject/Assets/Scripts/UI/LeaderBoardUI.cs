using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class LeaderBoardUI : MonoBehaviour
{
    [SerializeField] private string defaultPath = "/HIGHSCORES.txt";
    [SerializeField] private LeaderboardLineUI[] lines;
    
    private void Start()
    {
        HighscoreEntry[] entries;
        
        if (!File.Exists(Application.persistentDataPath + defaultPath))
        {
            //If no file - create file with empty entries and new player entry
            
            entries = HandleMissingFile();
        }
        else
        {
            //if file - read file
            
            StreamReader sr = new StreamReader(Application.persistentDataPath + defaultPath);
            string text = sr.ReadToEnd();
            sr.Close();
            
            HighscoreList highscoreList = JsonUtility.FromJson<HighscoreList>(text);
            entries = highscoreList.highscores;
        }
        
        UpdateUI(entries);
    }
    
    private HighscoreEntry[] HandleMissingFile()
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + defaultPath);

        HighscoreList highscoreList = new HighscoreList();

        for (int i = 0; i < highscoreList.highscores.Length; i++)
        {
            highscoreList.highscores[i] = new HighscoreEntry("Empty", -1f);
        }

        string json = JsonUtility.ToJson(highscoreList);

        sw.Write(json);
        sw.Close();

        return highscoreList.highscores;
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
