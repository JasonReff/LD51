using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string[] levels;
    public GameObject levelSelect;
    public GameObject optionsMenu;
    public GameObject credits;
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

    }

    public void HideSubmenus() {
        levelSelect.SetActive(false);
        optionsMenu.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(false);
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
            instructions.SetActive(true);
        }

    }

    public void DisplayCredits() {
        if (!credits.activeInHierarchy) {
            LightningStrike();
            HideSubmenus();
            credits.SetActive(true);
        }
    }
}
