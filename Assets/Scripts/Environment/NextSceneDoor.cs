using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneDoor : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private float _minimumDuration = 1f;
    public static event Action OnLevelFinished;
    public void MoveToScene()
    {
        Time.timeScale = 1;
        OnLevelFinished?.Invoke();
        SceneLoader.Instance.LoadScene(_sceneName, _minimumDuration);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            MoveToScene();
        }
    }
}
