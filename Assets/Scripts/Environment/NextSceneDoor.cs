using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneDoor : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public void MoveToScene()
    {
        SceneLoader.Instance.LoadScene(_sceneName, true);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerManager player))
        {
            MoveToScene();
        }
    }
}
