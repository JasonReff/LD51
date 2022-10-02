using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string[] levels;
    public Image Background;
    public Sprite OpenDoorBackground;
    public GameObject title;
    public GameObject levelSelect;
    public GameObject optionsMenu;
    public GameObject credits;
    public GameObject fontCredits;
    public GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightningStrike()
    {

    }

    public void OpenDoor()
    {
        Background.sprite = OpenDoorBackground;
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
        LightningStrike();
        OpenDoor();
        // time delay
        SceneManager.LoadScene(levels[number]);
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
