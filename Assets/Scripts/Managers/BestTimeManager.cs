using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BestTimeManager : SingletonMonobehaviour<BestTimeManager>
{
    [SerializeField] private float _time = 0f, _bestTime;
    [SerializeField] private LevelCompletionData _levelCompletionData;
    [SerializeField] private CharacterSelectData _characterSelectData;
    [SerializeField] private TextMeshProUGUI _currentTimeTextbox, _bestTimeTextbox;
    private string stageName;

    public float CurrentTime { get => _time; set => _time = value; }

    private void OnEnable()
    {
        NextSceneDoor.OnLevelFinished += RecordTime;
    }

    private void OnDisable()
    {
        NextSceneDoor.OnLevelFinished -= RecordTime;
    }

    private void RecordTime()
    {
        if (_time < _bestTime)
            _levelCompletionData.SetCompletionStatus(_characterSelectData.SelectedCharacter, stageName, true, _time);

    }

    private void Start()
    {
        stageName = SceneManager.GetActiveScene().name;
        _bestTime = _levelCompletionData.PullBestTime(_characterSelectData.SelectedCharacter, stageName);
        _bestTimeTextbox.text = _bestTime.ToString();
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
        if (_time < _bestTime)
            _currentTimeTextbox.color = Color.green;
        else _currentTimeTextbox.color = Color.red;
        _currentTimeTextbox.text = _time.ToString();
    }
}
