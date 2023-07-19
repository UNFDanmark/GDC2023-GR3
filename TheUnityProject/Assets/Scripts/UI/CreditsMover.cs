using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMover : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private RectTransform creditsBottomTransform;
    [SerializeField] private RectTransform screenTopPoint;
    
    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);

        if (creditsBottomTransform.position.y >= screenTopPoint.position.y)
        {
            SceneHandler.Instance.LoadScene(SceneHandler.Instance.mainMenuSceneIndex, MusicPlayer.Instance.menuMusic, SceneLoadType.Load);
        }
    }
}
