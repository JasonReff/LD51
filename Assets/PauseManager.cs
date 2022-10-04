using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas _pauseCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Resume();
            }
            else Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _pauseCanvas.gameObject.SetActive(true);
        AudioManager.PauseGameMusic();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseCanvas.gameObject.SetActive(false);
        AudioManager.UnpauseGameMusic();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.ReloadScene(true);
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}
