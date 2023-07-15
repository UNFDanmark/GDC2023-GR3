using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardHighscore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI timeText;

    public void SetText(string newName, string newTime)
    {
        gameObject.SetActive(true);
        
        nameText.text = newName;
        timeText.text = newTime;
    }
}
