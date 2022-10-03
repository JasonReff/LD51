using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    // fields
    public string[] levels;
    public Camera cam;
    private Color cameraDefaultColor;
    public Image background;
    private Color bgDefaultColor;
    public Sprite OpenDoorBackground;
    public GameObject title;
    public GameObject levelSelect;
    public GameObject optionsMenu;
    public GameObject credits;
    public GameObject fontCredits;
    public GameObject instructions;
    public TMP_Text toggleText;

    public bool post = true;

    // timing
    private bool timerActive = false;
    public float timerDuration;
    public float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        bgDefaultColor = background.color;
        cameraDefaultColor = cam.backgroundColor;
        currentTime = timerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime -= 1;
            float clerp = Mathf.Abs(1 - (currentTime / timerDuration));
            background.color = Color.Lerp(Color.white, bgDefaultColor, clerp);
            cam.backgroundColor = Color.Lerp(Color.white, cameraDefaultColor, clerp);

            if (currentTime <= 0)
            {
                timerActive = false;
                currentTime = timerDuration;
                resetColors();
            }
        }
    }

    public void resetColors() 
    {
        background.color = bgDefaultColor;
        cam.backgroundColor = cameraDefaultColor;
    }

    public void LightningStrike()
    {
        cam.backgroundColor = Color.white;
        background.color = Color.white;
        timerActive = true;
    }

    public void OpenDoor()
    {
        background.sprite = OpenDoorBackground;
    }

    public void HideSubmenus() {
        levelSelect.SetActive(false);
        optionsMenu.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(false);
        fontCredits.SetActive(false);
        title.SetActive(true);
    }

    public void LoadLevel(int number)
    {
        StartCoroutine(LoadLevelCoroutine());

        IEnumerator LoadLevelCoroutine()
        {
            LightningStrike();
            HideSubmenus();
            OpenDoor();
            yield return new WaitForSeconds(timerDuration / 60);
            SceneManager.LoadScene(levels[number]);

        }
    }

    public void TogglePostProc()
    {
        post = !post;
        if (post) toggleText.text = "on";
        else toggleText.text = "off";
    }

    public void DisplayLevelSelect()
    {
        if (!levelSelect.activeInHierarchy) {
            LightningStrike();
            HideSubmenus();
            levelSelect.SetActive(true);
        }
    }

    public void DisplayOptions() 
    {
        if (!optionsMenu.activeInHierarchy) {
            LightningStrike();
            HideSubmenus();
            optionsMenu.SetActive(true);
        }
    }

    public void DisplayInstructions()
    {
        if (!instructions.activeInHierarchy) {
            LightningStrike();
            HideSubmenus();
            title.SetActive(false);
            instructions.SetActive(true);
        }

    }

    public void DisplayCredits() {
        if (!credits.activeInHierarchy) {
            LightningStrike();
            HideSubmenus();
            title.SetActive(false);
            credits.SetActive(true);
            fontCredits.SetActive(true);
        }
    }
}
