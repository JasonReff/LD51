using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectLevel : MonoBehaviour
{
    [SerializeField] private string _stageName;
    public string StageName { get => _stageName; }
    [SerializeField] private LevelCompletionData _levelCompletionData;
    [SerializeField] private List<LevelSelectPlayButton> _playButtons;
    [SerializeField] private TextMeshProUGUI _levelTextbox, _timeTextbox;
    [SerializeField] private CharacterSelectData _characterSelectData;
    

    public void Initialize(string stageName)
    {
        _stageName = stageName;
        _levelTextbox.text = stageName.Substring(stageName.Length - 3);
        DisplayTime(_characterSelectData.SelectedCharacter);
    }

    private void OnEnable()
    {
        SettingsManager.OnCharacterSelected += DisplayTime;
    }

    private void OnDisable()
    {
        SettingsManager.OnCharacterSelected -= DisplayTime;
    }

    private void LoadCompletions()
    {
        var charactersCompleted = _levelCompletionData.GetCompletedCharacters(_stageName);
        foreach (var play in _playButtons)
        {
            if (charactersCompleted.Contains(play.CharacterData))
            {
                play.LoadCompletionSprite();
            }
        }
    }

    private void DisplayTime(CharacterData character)
    {
        if (_levelCompletionData.GetCompletionStatus(character, _stageName))
        {
            var completionTime = _levelCompletionData.PullBestTime(character, _stageName);
            _timeTextbox.text = completionTime.ToString("0.00");
        }
        else
        {
            _timeTextbox.text = "-- --";
        }
        
    }

    public void PlayLevel()
    {
        SceneLoader.Instance.LoadScene(_stageName);
    }
}
