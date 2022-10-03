using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    public static SceneLoader Instance { get; private set; }

    public void Awake()
    {
        if (Instance)
            Destroy(this);
        else
            Instance = this;
    }

    public void LoadScene(string sceneName)
    {
        LoadScene(sceneName, false);
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
        canvas.enabled = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        canvas.enabled = false;
    }

    IEnumerator LoadSceneAsync(string sceneName, float minimumDuration)
    {
        canvas.enabled = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        float time = 0;
        while (!asyncLoad.isDone && time <= minimumDuration)
        {
            time += Time.fixedUnscaledDeltaTime;
            yield return null;
        }

        canvas.enabled = false;
    }

    public void ReloadScene(bool async)
    {
        Time.timeScale = 1;
        string currentScene = SceneManager.GetActiveScene().name;
        LoadScene(currentScene, async);
    }

    public void ReloadScene(float minimumDuration)
    {
        Time.timeScale = 1;
        string currentScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAsync(currentScene, minimumDuration));
    }
}
