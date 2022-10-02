using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    public void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
    }

    public void LoadScene(string sceneName, bool async = false)
    {
        if (async)
            StartCoroutine(LoadSceneAsync(sceneName));
        else
            SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ReloadScene(bool async)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        LoadScene(currentScene, async);
    }
}
