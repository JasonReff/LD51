using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BestTimeManager : MonoBehaviour
{
    [SerializeField] private float _time = 0f, _bestTime;
    [SerializeField] private BestTimesData _timeData;
    [SerializeField] private TextMeshProUGUI _currentTimeTextbox, _bestTimeTextbox;
    private string stageName;

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
            _timeData.RecordBestTime(stageName, _time);

    }

    private void Start()
    {
        stageName = SceneManager.GetActiveScene().name;
        _bestTime = _timeData.PullBestTime(stageName);
        _bestTimeTextbox.text = _bestTime.ToString();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time < _bestTime)
            _currentTimeTextbox.color = Color.green;
        else _currentTimeTextbox.color = Color.red;
        _currentTimeTextbox.text = _time.ToString();
    }
}
