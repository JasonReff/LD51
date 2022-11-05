using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : SingletonMonobehaviour<SettingsManager>
{
    [SerializeField] private Slider _musicSlider, _effectsSlider, _masterVolumeSlider;
    [SerializeField] private SoundSettingsData _soundSettings;
    [SerializeField] private List<CharacterData> _characterDatas;
    [SerializeField] private CharacterSelectData _characterSelectData;
    [SerializeField] private Image _characterDisplay, _levelSelectCharacterDisplay;
    [SerializeField] private CameraSettingsData _cameraSettings;
    private int _characterDataIndex;
    public static event Action<CharacterData> OnCharacterSelected;

    private void Start()
    {
        _musicSlider.value = AudioManager.MusicVolume();
        _effectsSlider.value = AudioManager.EffectsVolume();
        _masterVolumeSlider.value = AudioManager.MasterVolume();
    }

    public void SetMusicVolume(float value)
    {
        _soundSettings.MusicVolume = value;
        AudioManager.SetMusicVolume(value);
    }

    public void SetEffectsVolume(float value)
    {
        _soundSettings.EffectsVolume = value;
        AudioManager.SetEffectsVolume(value);
    }

    public void SetMasterVolume(float value)
    {
        _soundSettings.MasterVolume = value;
        AudioManager.SetMasterVolume(value);
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
        UpdateCharacter(_characterDataIndex);
    }

    private void UpdateCharacter(int characterIndex)
    {
        var characterData = _characterDatas[characterIndex];
        _characterDisplay.sprite = characterData.CharacterSprite;
        _levelSelectCharacterDisplay.sprite = characterData.CharacterSprite;
        _characterSelectData.SelectedCharacter = characterData;
        OnCharacterSelected?.Invoke(characterData);
    }

    public void TogglePostProcessing()
    {
        _cameraSettings.PostProcessing = !_cameraSettings.PostProcessing;
    }
}
