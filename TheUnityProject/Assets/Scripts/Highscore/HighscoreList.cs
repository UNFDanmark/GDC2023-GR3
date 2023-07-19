using System;

[Serializable]
public class HighscoreList
{
    public HighscoreList()
    {
        highscores = new HighscoreEntry[10];
    }

    public string levelName;
    public HighscoreEntry[] highscores;
}
