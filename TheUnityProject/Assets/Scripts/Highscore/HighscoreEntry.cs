using System;

[Serializable]
public class HighscoreEntry
{
    public string name;
    public float time;

    public HighscoreEntry(string _name, float _time)
    {
        name = _name;
        time = _time;
    }
    
}
