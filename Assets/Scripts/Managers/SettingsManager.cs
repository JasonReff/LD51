using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider, _effectsSlider, _masterVolumeSlider;
    [SerializeField] private SoundSettingsData _soundSettings;
    [SerializeField] private List<CharacterData> _characterDatas;
    [SerializeField] private CharacterSelectData _characterSelectData;
    private int _characterDataIndex;

    private void Awake()
    {
        _musicSlider.value = AudioManager.MusicVolume();
        _effectsSlider.value = AudioManager.EffectsVolume();
        _masterVolumeSlider.value = AudioManager.MasterVolume();
    }

    public void SetMusicVolume(float value)
    {
        _soundSettings.MusicVolume = value;
    }

    public void SetEffectsVolume(float value)
    {
        _soundSettings.EffectsVolume = value;
    }

    public void SetMasterVolume(float value)
    {
        _soundSettings.MasterVolume = value;
    }

    public void CycleCharacterUp()
    {
        _characterDataIndex++;
        if (_characterDataIndex == _characterDatas.Count)
            _characterDataIndex = 0;
        UpdateCharacter(_characterDataIndex);
    }

    public void CycleCharacterDown()
    {
        _characterDataIndex--;
        if (_characterDataIndex < 0)
            _characterDataIndex = _characterDatas.Count - 1;

    }

    private void UpdateCharacter(int characterIndex)
    {
        var characterData = _characterDatas[_characterDataIndex];
    }
}