using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPlayButton : MonoBehaviour
{
    [SerializeField] private LevelSelectLevel _levelSelectLevel;
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private CharacterSelectData _characterSelectData;
    [SerializeField] private Image _completionImage;
    [SerializeField] private TextMeshProUGUI _completionTime;
    
    public CharacterData CharacterData { get => _characterData; }


    public void LoadCompletionSprite()
    {
        _completionImage.enabled = true;
    }

    public void LoadCompletionTime(float time)
    {
        _completionTime.enabled = true;
        _completionTime.text = time.ToString("0.00");
    }

    public void PlayLevel()
    {
        _characterSelectData.SelectedCharacter = _characterData;
        SceneLoader.Instance.LoadScene(_levelSelectLevel.StageName);
    }
}
