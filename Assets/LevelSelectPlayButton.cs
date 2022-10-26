using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPlayButton : MonoBehaviour
{
    [SerializeField] private LevelSelectLevel _levelSelectLevel;
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private CharacterSelectData _characterSelectData;
    [SerializeField] private Image _completionImage;
    
    public CharacterData CharacterData { get => _characterData; }


    public void LoadCompletionSprite()
    {
        _completionImage.enabled = true;
    }

    public void PlayLevel()
    {
        _characterSelectData.SelectedCharacter = _characterData;
        SceneLoader.Instance.LoadScene(_levelSelectLevel.StageName);
    }
}
