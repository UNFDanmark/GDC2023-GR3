using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderboardLineUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI nameText;
    [SerializeField] public TextMeshProUGUI timeText;
    [SerializeField] public TMP_InputField nameInput;

    private void Awake()
    {
        nameInput.gameObject.SetActive(false);
    }

    public void SetText(string newName, string newTime)
    {
        gameObject.SetActive(true);
        
        nameText.text = newName;
        timeText.text = newTime;
    }
}
