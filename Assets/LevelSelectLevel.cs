using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectLevel : MonoBehaviour
{
    [SerializeField] private string _stageName;
    public string StageName { get => _stageName; }
    [SerializeField] private BestTimesData _bestTimesData;
    [SerializeField] private LevelCompletionData _levelCompletionData;
    [SerializeField] private List<LevelSelectPlayButton> _playButtons;
    [SerializeField] private TextMeshProUGUI _levelTextbox, _timeTextbox;

    public void Initialize(string stageName)
    {
        _stageName = stageName;
        _timeTextbox.text = _bestTimesData.PullBestTime(stageName).ToString();
        _levelTextbox.text = stageName.Substring(stageName.Length - 3);
        LoadCompletionSprites();
    }

    private void LoadCompletionSprites()
    {
        var charactersCompleted = _levelCompletionData.GetCompletedCharacters(_stageName);
        foreach (var play in _playButtons)
        {
            if (charactersCompleted.Contains(play.CharacterData))
                play.LoadCompletionSprite();
        }
    }
}
