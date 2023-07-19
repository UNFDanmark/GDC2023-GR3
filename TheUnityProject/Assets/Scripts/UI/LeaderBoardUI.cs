using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LeaderBoardUI : MonoBehaviour
{
    private const string PLAYERPREFS_SCENENAME = "SceneName";
    
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private string[] paths;
    [SerializeField] private LeaderboardLineUI[] lines;

    private int pathIndex;
    
    private void Start()
    {
        SetUpButtons();

        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        HighscoreEntry[] entries;
        
        if (!File.Exists(Application.persistentDataPath + "/" + paths[pathIndex] + ".txt"))
        {
            //If no file - create file with empty entries and new player entry
            
            entries = HandleMissingFile(paths[pathIndex]);
        }
        else
        {
            //if file - read file
            
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/" + paths[pathIndex] + ".txt");
            string text = sr.ReadToEnd();
            sr.Close();
            
            HighscoreList highscoreList = JsonUtility.FromJson<HighscoreList>(text);
            entries = highscoreList.highscores;
        }
        
        UpdateUI(entries);
    }
    
    private void SetUpButtons()
    {
        backButton.onClick.AddListener(() =>
        {
            pathIndex--;

            if (pathIndex < 0)
            {
                pathIndex = paths.Length - 1;
            }
            
            LoadLeaderboard();
        });
        nextButton.onClick.AddListener(() =>
        {
            pathIndex++;

            if (pathIndex >= paths.Length)
            {
                pathIndex = 0;
            }
            
            LoadLeaderboard();
        });
    }
    
    private HighscoreEntry[] HandleMissingFile(string sceneName)
    {
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + paths[pathIndex] + ".txt");

        HighscoreList highscoreList = new HighscoreList();

        highscoreList.levelName = sceneName;
        
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
