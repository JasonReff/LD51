using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

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
    public SceneLoader SceneLoader;

    public bool post = true;

    // timing
    private bool timerActive = false;
    public float timerDuration;
    public float currentTime;
    public float cameraTweenTime = 1f, cameraTweenDelay = 0.5f;

    //Effects
    [SerializeField] private ThunderManager _thunder;

    //Settings
    [SerializeField] private CameraSettingsData _cameraSettings;
    [SerializeField] private CameraSettingsReader _cameraSettingsReader;


    // Start is called before the first frame update
    void Start()
    {
        bgDefaultColor = background.color;
        cameraDefaultColor = cam.backgroundColor;
        currentTime = timerDuration;
        post = _cameraSettings.PostProcessing;
        if (post) toggleText.text = "on";
        else toggleText.text = "off";
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
        _thunder.PlayThunder();
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

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelCoroutine());

        IEnumerator LoadLevelCoroutine()
        {
            LightningStrike();
            HideSubmenus();
            OpenDoor();
            yield return new WaitForSeconds(timerDuration / 60);
            SceneLoader.LoadScene(levelName);

        }
    }

    public void TogglePostProc()
    {
        post = !post;
        if (post) toggleText.text = "on";
        else toggleText.text = "off";
        _cameraSettings.PostProcessing = post;
        _cameraSettingsReader.UpdateSettings();
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

    public void TweenCamera(Camera otherCamera)
    {
        StartCoroutine(Coroutine());

        IEnumerator Coroutine()
        {
            yield return new WaitForSeconds(cameraTweenDelay);
            cam.transform.DOMove(otherCamera.transform.position, cameraTweenTime);
            cam.DOOrthoSize(otherCamera.orthographicSize, cameraTweenTime);
        }
    }
}
