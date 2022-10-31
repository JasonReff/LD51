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

    public void Initialize(string stageName)
    {
        _stageName = stageName;
        _levelTextbox.text = stageName.Substring(stageName.Length - 3);
        LoadCompletions();
    }

    private void LoadCompletions()
    {
        var charactersCompleted = _levelCompletionData.GetCompletedCharacters(_stageName);
        foreach (var play in _playButtons)
        {
            if (charactersCompleted.Contains(play.CharacterData))
            {
                play.LoadCompletionSprite();
                play.LoadCompletionTime(_levelCompletionData.PullBestTime(play.CharacterData, _stageName));
            }
        }
    }
}
