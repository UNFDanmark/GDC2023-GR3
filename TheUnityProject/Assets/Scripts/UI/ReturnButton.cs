using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    // Start is called before the first frame update
    void Start()
    {
        returnButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

}
