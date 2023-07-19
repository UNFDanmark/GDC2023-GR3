using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWinArea : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.levelOneSceneIndex, MusicPlayer.Instance.gameMusic, SceneLoadType.Load);
        }
    }
}
